using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//
namespace RayCaster
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();

            int frameHeight = 900;
            int frameWidth = 1600;
            int bytesPerPixel = 4;


            rayCaster objRaycaster = new rayCaster(frameHeight, frameWidth, bytesPerPixel, ref viewPort);

            Thread renderManager = new Thread(new ThreadStart(objRaycaster.renderLoop));
            renderManager.Start();
            





            /*
            Trace.Write("BeginDispatch");
            viewPort.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new rayCasterDelegate(objRaycaster.beginRender)
                );

            Trace.Write("EndDispatch");
            */


        }
    }



}
