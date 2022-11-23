using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Xml;
using FluentAssertions.Equivalency;
using FluentAssertions.Equivalency.Steps;
using Graphs;
using NUnit.Framework;

namespace Bondoc_Graph_Visualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Brush customColor;
        Random r = new Random();
        Queue<char> Names = new Queue<char>();
        char[] NamesStorage = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        List<CanvasVertex> Vertices = new List<CanvasVertex>();
        List<Edge> Edges = new List<Edge>();        
        LinkedList<string> PathList = new LinkedList<string>();
        List<string> VertexStorage = new List<string>();
        int EdgesCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            foreach (char i in NamesStorage)
            {
                Names.Enqueue(i);
            }
        }

        private void Add(object sender, MouseButtonEventArgs e)
        {
            if (Names.Count == 0) MessageBox.Show("Max Number of Vertices");
            else if (AddVerticesToggle.IsChecked == true)
            {
                char NamePointer = Names.Dequeue();
                customColor = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 255)));
                Ellipse VertexCircle = new Ellipse
                {
                    Name = Convert.ToString(NamePointer),
                    Width = 40,
                    Height = 40,
                    Fill = customColor,
                    StrokeThickness = 3,
                    Stroke = Brushes.Black
                };
                TextBlock VertexLetter = new TextBlock
                {
                    Name = Convert.ToString(NamePointer),
                    Text = Convert.ToString(NamePointer),
                    FontSize = 30,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10,0,0,0)
                };
                Canvas.SetLeft(VertexCircle, Mouse.GetPosition(GraphMap).X);
                Canvas.SetTop(VertexCircle, Mouse.GetPosition(GraphMap).Y);
                Canvas.SetLeft(VertexLetter, Mouse.GetPosition(GraphMap).X);
                Canvas.SetTop(VertexLetter, Mouse.GetPosition(GraphMap).Y);
                GraphMap.Children.Add(VertexCircle);
                GraphMap.Children.Add(VertexLetter);
                var newVertex = new CanvasVertex(NamePointer, Mouse.GetPosition(GraphMap).X, Mouse.GetPosition(GraphMap).Y);
                Vertices.Add(newVertex);
                VertexStorage.Add(NamePointer.ToString());
                InputVertexA.ItemsSource = InputVertexB.ItemsSource = InputStart.ItemsSource = InputEnd.ItemsSource = VertexStorage;
                addVerticesInTextBox(NamePointer.ToString());
                VerticesDisplay.Content = Vertices.Count.ToString();
            }                                               
        }

        public void AddEdge_Click(object sender, RoutedEventArgs e)
        {
            string VertexA = InputVertexA.Text;
            string VertexB = InputVertexB.Text;
            if (string.IsNullOrEmpty(InputWeight.Text) || string.IsNullOrEmpty(VertexA) || string.IsNullOrEmpty(VertexB) || VertexA == VertexB) MessageBox.Show("Invalid Input");
            else
            {
                int Weight = Convert.ToInt32(InputWeight.Text);
                int VertexAIndex = Vertices.FindIndex(i => i.Letter == Convert.ToChar(VertexA));
                int VertexBIndex = Vertices.FindIndex(i => i.Letter == Convert.ToChar(VertexB));
                string EdgeName = VertexA + VertexB;

                Line EdgeLine = new Line
                {
                    Name = EdgeName,
                    Stroke = Brushes.Black,
                    Fill = Brushes.Black,
                    StrokeThickness = 3,
                    X1 = Vertices[VertexAIndex].positionX + 15,
                    Y1 = Vertices[VertexAIndex].positionY + 15,
                    X2 = Vertices[VertexBIndex].positionX + 15,
                    Y2 = Vertices[VertexBIndex].positionY + 15
                };
                TextBlock WeightText = new TextBlock
                {                  
                    Text = InputWeight.Text,
                    FontSize = 15,
                    VerticalAlignment = VerticalAlignment.Center,
                    Background = new SolidColorBrush(Colors.White)
                };
                Ellipse WeightBackground = new Ellipse
                {                  
                    Width = 30,
                    Height = 30,
                    Fill = Brushes.White,
                    StrokeThickness = 3,
                    Stroke = Brushes.White
                };
                var LineMidPointX = (Vertices[VertexAIndex].positionX + Vertices[VertexBIndex].positionX) / 2;
                var LineMidPointY = (Vertices[VertexAIndex].positionY + Vertices[VertexBIndex].positionY) / 2;               
                Canvas.SetLeft(WeightText, LineMidPointX);
                Canvas.SetTop(WeightText, LineMidPointY);
                Canvas.SetLeft(WeightBackground, LineMidPointX);
                Canvas.SetTop(WeightBackground, LineMidPointY);
                GraphMap.Children.Insert(0, WeightText);
                GraphMap.Children.Insert(0, WeightBackground);
                GraphMap.Children.Insert(0, EdgeLine);
                Edges.Add(new Edge(VertexAIndex, VertexBIndex, Weight));
                Edges.Add(new Edge(VertexBIndex, VertexAIndex, Weight));
                InputWeight.Clear();
                EdgesCount++;
                EdgesDisplay.Content = Convert.ToString(EdgesCount);
            }          
        }

        private void ClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            Edges = new List<Edge>();
            Vertices = new List<CanvasVertex>();          
            GraphMap.Children.Clear();
            Names = new Queue<char>();
            foreach (char i in NamesStorage)
            {
                Names.Enqueue(i);
            }
            VertexStorage = new List<string>();
            InputVertexA.ItemsSource = InputVertexB.ItemsSource = InputStart.ItemsSource = InputEnd.ItemsSource = VertexStorage;
            VerticesNote.Clear();
            VerticesDisplay.Content = Vertices.Count.ToString();
            EdgesDisplay.Content = "0";
        }
      
        public bool CheckIfNamesAreEqual (string a, string b)
        {
            bool result = false;
            foreach(char c in a)
            {
                if (b.Contains(c) == true) result = true;
                else
                {
                    result = false; 
                    break;
                }            
            }
            return result;
        }

        private void FindShortestPath_Click(object sender, RoutedEventArgs e)
        {
            PathList = new LinkedList<string>();
            IEnumerable<Line> EdgeLines = GraphMap.Children.OfType<Line>();
            foreach (Line line in EdgeLines)
            {
                line.Fill = Brushes.Black;
                line.Stroke = Brushes.Black;
            }
            var newGraph = new GraphSirDex<CanvasVertex>(Vertices, Edges);
            char InputStartChar = Convert.ToChar(InputStart.Text);
            char InputEndChar = Convert.ToChar(InputEnd.Text);
            int StartIndex = Vertices.FindIndex(i => i.Letter == InputStartChar);
            int EndIndex = Vertices.FindIndex(i => i.Letter == InputEndChar);                    
            if (InputStart.Text == null || InputEnd.Text == null) MessageBox.Show("Invalid Input");
            else
            {
                var pathResult = newGraph.GetShortestPath(StartIndex);               
                int tmpIndex = EndIndex;
                while (tmpIndex != -1)
                {
                    char EdgeTraversedA = Vertices[tmpIndex].Letter;
                    tmpIndex = pathResult.Prev[tmpIndex];
                    if (tmpIndex == -1) break;
                    char EdgeTraversedB = Vertices[tmpIndex].Letter;
                    string Result = "" + EdgeTraversedA + EdgeTraversedB;
                    PathList.AddLast(Result);
                }
                foreach (var line in EdgeLines)
                {
                    var tmp = PathList.First;
                    while (tmp != null)
                    {
                        if (CheckIfNamesAreEqual(line.Name, tmp.Value) == true)
                        {
                            line.Fill = Brushes.Green;
                            line.Stroke = Brushes.Green;
                            tmp = tmp.Next;
                        }
                        else tmp = tmp.Next;
                    }                   
                }               
            }
        }
        private void addVerticesInTextBox(string vertexName)
        {        
                string value = vertexName + " =";
                VerticesNote.AppendText(value);
                VerticesNote.AppendText(Environment.NewLine);           
        }
    }
}
