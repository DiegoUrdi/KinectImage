using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;

namespace KinectImage
{
    public partial class KImage : Form
    {
        private KinectSensor _kSensor = null;
        private MultiSourceFrameReader _fReader = null;
        private int _maxIR = -1;
        private int _minIR = int.MaxValue;
        private int _scIR = 0;
        private bool _bProcessing = false;

        public KImage()
        {
            InitializeComponent();
        }

        private void DrawColorImage(ColorFrame cframe)
        {
            if (cframe != null)
            {
                Bitmap cbmp = null;
                BitmapData bd = null;
                try
                {
                    pbColor.Width = cframe.FrameDescription.Width == 1920 ? 853 : 640;
                    cbmp = new Bitmap(cframe.FrameDescription.Width, cframe.FrameDescription.Height);
                    bd = cbmp.LockBits(new Rectangle(0, 0, cframe.FrameDescription.Width, cframe.FrameDescription.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                    byte[] image = new byte[4 * cframe.FrameDescription.Width * cframe.FrameDescription.Height];


                    if (cframe.RawColorImageFormat == ColorImageFormat.Bgra)
                    {
                        cframe.CopyRawFrameDataToArray(image);
                    }
                    else
                    {
                        cframe.CopyConvertedFrameDataToArray(image, ColorImageFormat.Bgra);
                    }
                    Marshal.Copy(image, 0, bd.Scan0, image.Length);
                    cbmp.UnlockBits(bd);
                    bd = null;
                    pbColor.Image = cbmp;
                    //Console.WriteLine("caca");

                    cbmp.Save("b.bmp" , ImageFormat.Bmp);
                }
                catch
                {
                }
                finally
                {
                    if ((cbmp != null) && (bd != null))
                    {
                        cbmp.UnlockBits(bd);
                    }
                }
            }
        }
        private void DrawInfraredImage(InfraredFrame iframe)
        {
            if (iframe != null)
            {
                Bitmap ibmp = null;
                BitmapData bd = null;
                try
                {
                    ushort[] idata = new ushort[iframe.FrameDescription.Width * iframe.FrameDescription.Height];
                    iframe.CopyFrameDataToArray(idata);
                    if (_maxIR < 0)
                    {
                        for (int ix = 0; ix < idata.Length; ix++)
                        {
                            _maxIR = Math.Max(_maxIR, idata[ix]);
                            _minIR = Math.Min(_minIR, idata[ix]);
                        }
                        _scIR = _maxIR - _minIR;
                    }
                    else
                    {
                        ibmp = new Bitmap(iframe.FrameDescription.Width, iframe.FrameDescription.Height);
                        bd = ibmp.LockBits(new Rectangle(0, 0, iframe.FrameDescription.Width, iframe.FrameDescription.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                        int[] ipixels = new int[iframe.FrameDescription.Width * iframe.FrameDescription.Height];
                        for (int ix = 0; ix < idata.Length; ix++)
                        {
                            int pxint = (255 * (idata[ix] - _minIR)) / _scIR;
                            ipixels[ix] = Color.FromArgb(pxint, pxint, pxint).ToArgb();
                        }
                        Marshal.Copy(ipixels, 0, bd.Scan0, ipixels.Length);
                        ibmp.UnlockBits(bd);
                        bd = null;
                        pbInfrared.Image = ibmp;
                    }
                }
                catch
                {
                }
                finally
                {
                    if ((ibmp != null) && (bd != null))
                    {
                        ibmp.UnlockBits(bd);
                    }
                }
            }
        }
        private void DrawDepthImage(DepthFrame dframe)
        {
            if (dframe != null)
            {
                Bitmap dbmp = null;
                BitmapData bd = null;
                try
                {
                    int[] bdepth = new int[dframe.FrameDescription.Width * dframe.FrameDescription.Height];
                    ushort[] bw = new ushort[dframe.FrameDescription.Width * dframe.FrameDescription.Height];
                    dframe.CopyFrameDataToArray(bw);
                    ushort dmax = dframe.DepthMaxReliableDistance;
                    ushort dmin = dframe.DepthMinReliableDistance;
                    int rec = dmax - dmin;
                    for (int ix = 0; ix < bw.Length; ix++)
                    {
                        int dpixel = (bw[ix] >= dmin && bw[ix] <= dmax) ? (256 * (bw[ix] - dmin)) / rec : 0;
                        bdepth[ix] = Color.FromArgb(dpixel, dpixel, dpixel).ToArgb();
                    }
                    dbmp = new Bitmap(dframe.FrameDescription.Width, dframe.FrameDescription.Height);
                    bd = dbmp.LockBits(new Rectangle(0, 0, dframe.FrameDescription.Width, dframe.FrameDescription.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                    Marshal.Copy(bdepth, 0, bd.Scan0, bdepth.Length);
                    dbmp.UnlockBits(bd);
                    bd = null;
                    pbDepth.Image = dbmp;
                }
                catch
                {
                }
                finally
                {
                    if ((dbmp != null) && (bd != null))
                    {
                        dbmp.UnlockBits(bd);
                    }
                }
            }
        }
        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            if (!e.IsAvailable)
            {
                bCapture.Enabled = false;
                bStop.Enabled = false;
            }
            else
            {
                bCapture.Enabled = !_kSensor.IsOpen;
                bStop.Enabled = _kSensor.IsOpen;
            }
        }
        private void Image_FrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            if (!_bProcessing)
            {
                try
                {
                    _bProcessing = true;
                    MultiSourceFrame frame = e.FrameReference.AcquireFrame();
                    if (frame == null)
                    {
                        return;
                    }
                    using (ColorFrame cframe = frame.ColorFrameReference.AcquireFrame())
                    {
                        DrawColorImage(cframe);
                    }
                    using (InfraredFrame iframe = frame.InfraredFrameReference.AcquireFrame())
                    {
                        DrawInfraredImage(iframe);
                    }
                    using (DepthFrame dframe = frame.DepthFrameReference.AcquireFrame())
                    {
                        DrawDepthImage(dframe);
                    }
                }
                catch
                {
                }
                finally
                {
                    Refresh();
                    Application.DoEvents();
                    _bProcessing = false;
                }
            }
        }
        private void bCapture_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_kSensor.IsOpen)
                {
                    _kSensor.Open();
                    _maxIR = -1;
                    _minIR = int.MaxValue;
                    _bProcessing = false;
                    _fReader = _kSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared);
                    _fReader.MultiSourceFrameArrived += new EventHandler<MultiSourceFrameArrivedEventArgs>(Image_FrameArrived);
                }
                bCapture.Enabled = false;
                bStop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void KImage_Load(object sender, EventArgs e)
        {
            if (_kSensor == null)
            {
                _kSensor = KinectSensor.GetDefault();
                _kSensor.IsAvailableChanged += new EventHandler<IsAvailableChangedEventArgs>(Sensor_IsAvailableChanged);
            }
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_kSensor != null)
                {
                    _kSensor.Close();
                    _fReader.Dispose();
                    _fReader = null;
                }
                bCapture.Enabled = true;
                bStop.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
