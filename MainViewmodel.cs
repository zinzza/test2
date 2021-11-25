using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apparelbase;
using System.Windows.Media;
using Apparelbase.Windows.Input;
using System.Windows.Input;
using System.Windows;

namespace CombineTest
{
    public class MainViewmodel : ObservableObject
    {
        #region Goemetry1
        private PathGeometry geometry1;
        public PathGeometry Geometry1
        {
            get { return this.geometry1; }
            set
            {
                if (this.geometry1 != value)
                {
                    this.geometry1 = value;
                    this.RaisePropertyChanged("Geometry1");
                }
            }
        }
        #endregion

        #region Geometry2
        private Geometry geometry2;
        public Geometry Geometry2
        {
            get { return this.geometry2; }
            set
            {
                if (this.geometry2 != value)
                {
                    this.geometry2 = value;
                    this.RaisePropertyChanged("Geometry2");
                }
            }
        }
        #endregion

        #region Geometry3
        private PathGeometry geometry3;
        public PathGeometry Geometry3
        {
            get { return this.geometry3; }
            set
            {
                if (this.geometry3 != value)
                {
                    this.geometry3 = value;
                    this.RaisePropertyChanged("Geometry3");
                }
            }
        }
        #endregion
        List<PathSegment> segs = new List<PathSegment>();

        private void Combine()
        {
            this.Geometry2 = new RectangleGeometry(new Rect(new Point(10, 100), new Point(200, 200)));
            this.Geometry3 = Geometry.Combine(Geometry1, Geometry2, GeometryCombineMode.Intersect, null);
            foreach (var f in this.Geometry3.Figures)
            {
                foreach (var seg in f.Segments)
                {
                    segs.Add(seg);
                }
            }
        }

        #region Test1Command
        //rectangle, linegeometry
        private ICommand test1Command;
        public ICommand Test1Command
        {
            get { return (this.test1Command) ?? (this.test1Command = new DelegateCommand(Test1)); }
        }

        private void Test1()
        {
            this.Geometry1 = new PathGeometry();
            this.Geometry1.AddGeometry(new LineGeometry(new Point(50, 50), new Point(150, 180)));
            Combine();
        }
        #endregion

        #region Test2Command
        private ICommand test2Command;
        public ICommand Test2Command
        {
            get { return (this.test2Command) ?? (this.test2Command = new DelegateCommand(Test2)); }
        }

        private void Test2()
        {
            MakePolyLine();
            Combine();
        }

        private void MakePolyLine()
        {
            this.Geometry1 = new PathGeometry();
            PolyLineSegment seg = new PolyLineSegment(new List<Point>() { new Point(80, 80), new Point(100, 160), new Point(150, 180) }, true);
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(50, 50);
            figure.Segments.Add(seg);
            figure.IsClosed = false;

            PathGeometry p = new PathGeometry();
            p.Figures.Add(figure);

            this.Geometry1 = p;
        }
        #endregion

        #region Test3Command
        private ICommand test3Command;
        public ICommand Test3Command
        {
            get { return (this.test3Command) ?? (this.test3Command = new DelegateCommand(Test3)); }
        }

        private void Test3()
        {
            MakePolyLine();
            this.Geometry1.Figures.First().IsClosed = true;
            Combine();
        }
        #endregion

        #region Test4Command
        private ICommand test4Command;
        public ICommand Test4Command
        {
            get { return (this.test4Command) ?? (this.test4Command = new DelegateCommand(Test4)); }
        }

        private void Test4()
        {
            MakeZeroWidthPolygon();
            Combine();
        }

        private void MakeZeroWidthPolygon()
        {
            this.Geometry1 = new PathGeometry();
            PolyLineSegment seg = new PolyLineSegment(new List<Point>() { new Point(80, 80), new Point(100, 160), new Point(150, 180), new Point(100, 160), new Point(80, 80) }, true);
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(50, 50);
            figure.Segments.Add(seg);
            figure.IsClosed = true;

            PathGeometry p = new PathGeometry();
            p.Figures.Add(figure);

            this.Geometry1 = p;
        }

        #endregion

        #region Test5Command
        private ICommand test5Command;
        public ICommand Test5Command
        {
            get { return (this.test5Command) ?? (this.test5Command = new DelegateCommand(Test5)); }
        }

        private void Test5()
        {
            MakeZeroWidthLine();
            Combine();
        }

        private void MakeZeroWidthLine()
        {
            this.Geometry1 = new PathGeometry();
            PolyLineSegment seg = new PolyLineSegment(new List<Point>() { new Point(150, 180), new Point(50, 50) }, true);
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(50, 50);
            figure.Segments.Add(seg);
            figure.IsClosed = true;

            PathGeometry p = new PathGeometry();
            p.Figures.Add(figure);

            this.Geometry1 = p;
        }
        #endregion


        #region Test6Command
        private ICommand test6Command;
        public ICommand Test6Command
        {
            get { return (this.test6Command) ?? (this.test6Command = new DelegateCommand(Test6)); }
        }

        private void Test6()
        {
            MakeBezier();
            Combine();
            GetBezierSegs();
        }

        private void MakeBezier()
        {
            this.Geometry1 = new PathGeometry();
            BezierSegment seg = new BezierSegment(new Point(10, 80), new Point(60, 160), new Point(150, 180), true);
            //PolyBezierSegment seg2 = new PolyBezierSegment();
            //seg2.Points.Add(new Point(10, 20));
            //seg2.Points.Add(new Point(80,20));
            //seg2.Points.Add(new Point(100,200));
            //seg2.Points.Add(new Point(100,170));
            //seg2.Points.Add(new Point(170,200));
            //seg2.Points.Add(new Point(200,10));

            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(50, 50);
            figure.Segments.Add(seg);
            //figure.Segments.Add(seg2);
            figure.IsClosed = false;

            PathGeometry p = new PathGeometry();
            p.Figures.Add(figure);

            this.Geometry1 = p;
        }
        #endregion

        #region Test7Command
        private ICommand test7Command;
        public ICommand Test7Command
        {
            get { return (this.test7Command) ?? (this.test7Command = new DelegateCommand(Test7)); }
        }

        private void Test7()
        {
            MakeZeroWidthBezier();
            Combine();
        }
        #endregion

        private void MakeZeroWidthBezier()
        {
            this.Geometry1 = new PathGeometry();
            BezierSegment seg = new BezierSegment(new Point(10, 80), new Point(60, 160), new Point(150, 180), true);
            BezierSegment seg2 = new BezierSegment(new Point(60, 160), new Point(10, 80), new Point(50,50),true);
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(50, 50);
            figure.Segments.Add(seg);
            figure.Segments.Add(seg2);
            figure.IsClosed = false;

            PathGeometry p = new PathGeometry();
            p.Figures.Add(figure);

            this.Geometry1 = p;
        }

        private void GetBezierSegs()
        {
            PathGeometry path = new PathGeometry();
            Point lastPt = new Point();
            foreach (var f in this.Geometry3.Figures)
            {
                lastPt = f.StartPoint;
                foreach (var s in f.Segments)
                {
                    if (s is BezierSegment )
                    {
                        PathFigure figure = new PathFigure();
                        figure.StartPoint = lastPt;
                        figure.Segments.Add(s);
                        path.Figures.Add(figure);
                        lastPt = (s as BezierSegment).Point3;
                    }
                    else if (s is PolyBezierSegment)
                    {
                        PathFigure figure = new PathFigure();
                        figure.StartPoint = lastPt;
                        figure.Segments.Add(s);
                        path.Figures.Add(figure);
                        lastPt = (s as PolyBezierSegment).Points.Last();
                    }
                    else if (s is PolyLineSegment)
                    {
                        PathFigure figure = new PathFigure();
                        figure.StartPoint = lastPt;
                        figure.Segments.Add(s);
                        path.Figures.Add(figure);
                        lastPt = (s as PolyLineSegment).Points.Last();
                    }
                    else if (s is LineSegment)
                    {
                        PathFigure figure = new PathFigure();
                        figure.StartPoint = lastPt;
                        figure.Segments.Add(s);
                        path.Figures.Add(figure);
                        lastPt = (s as LineSegment).Point;
                    }
                }
            }
            this.Geometry3 = path;
        }

        #region Test8Command
        private ICommand test8Command;
        public ICommand Test8Command
        {
            get { return (this.test8Command) ?? (this.test8Command = new DelegateCommand(Test8)); }
        }

        private void Test8()
        {
            MakePolyBezier();
            Combine();
            GetBezierSegs();
        }

        private void MakePolyBezier()
        {
            this.Geometry1 = new PathGeometry();
            //BezierSegment seg = new BezierSegment(new Point(10, 80), new Point(60, 160), new Point(150, 180), true);
            PolyBezierSegment seg2 = new PolyBezierSegment();
            seg2.Points.Add(new Point(10, 80)   );
            seg2.Points.Add(new Point(60, 160)  );
            seg2.Points.Add(new Point(150, 180));
            seg2.Points.Add(new Point(10, 20));
            seg2.Points.Add(new Point(80, 20));
            seg2.Points.Add(new Point(100, 200));
            seg2.Points.Add(new Point(100, 170));
            seg2.Points.Add(new Point(170, 200));
            seg2.Points.Add(new Point(200, 10));

            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(50, 50);
            //figure.Segments.Add(seg);
            figure.Segments.Add(seg2);
            figure.IsClosed = false;

            PathGeometry p = new PathGeometry();
            p.Figures.Add(figure);

            this.Geometry1 = p;
        }
        #endregion

        #region Test9Command
        private ICommand test9Command;
        public ICommand Test9Command
        {
            get { return (this.test9Command) ?? (this.test9Command = new DelegateCommand(Test9)); }
        }

        private void Test9()
        {
            MakePolyBezier();
            CombineFromBezierSegment();
            GetBezierSegs();
        }

        private void CombineFromBezierSegment()
        {
            this.Geometry2 = new RectangleGeometry(new Rect(new Point(10, 100), new Point(200, 200)));

            PathGeometry path = new PathGeometry();
            Point lastPt = new Point();
            foreach (var f in this.Geometry1.Figures)
            {
                lastPt = f.StartPoint;
                foreach (var s in f.Segments)
                {
                    if (s is BezierSegment)
                    {
                        PathFigure figure = new PathFigure();
                        figure.StartPoint = lastPt;
                        figure.Segments.Add(s);
                        path.Figures.Add(figure);
                        lastPt = (s as BezierSegment).Point3;
                    }
                    else if (s is PolyBezierSegment)
                    {
                        PathFigure figure = new PathFigure();
                        figure.StartPoint = lastPt;
                        figure.Segments.Add(s);
                        path.Figures.Add(figure);
                        lastPt = (s as PolyBezierSegment).Points.Last();
                    }
                    else if (s is PolyLineSegment)
                    {
                        lastPt = (s as PolyLineSegment).Points.Last();
                    }
                    else if (s is LineSegment)
                    {
                        lastPt = (s as LineSegment).Point;
                    }
                }
            }

            //this.Geometry3 = path; 
            this.Geometry3 = Geometry.Combine(path, Geometry2, GeometryCombineMode.Intersect, null);
        }
        #endregion
    }


}
