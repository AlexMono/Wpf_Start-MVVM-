using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Wpf_Start_MVVM.Controls
{
    public class PathButton : Button
    {
        public static DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(Geometry), typeof(PathButton), new FrameworkPropertyMetadata(new PropertyChangedCallback(Data_Changed)));

        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        private static void Data_Changed(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            PathButton thisClass = (PathButton)o;
            thisClass.SetData();
        }

        private void SetData()
        {
            Path path = new Path();
            path.Data = Data;
            path.Stroke = this.Foreground;
            path.StrokeThickness = 1;            
            this.Content = path;
        }
    }
}
