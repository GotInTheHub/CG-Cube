using CG_Cube.Algebra;
using CG_Cube.Axes;
using CG_Cube.Shapes;
using MatrixTransformations;
using System.Reflection;
using System.Timers;

namespace CG_Cube
{
    public partial class Form1 : Form
    {
        // Window dimensions
        const int WIDTH = 1600;
        const int HEIGHT = 900;
        readonly Vector CENTER = new(WIDTH * 0.5f, HEIGHT * 0.5f);

        // Axes
        readonly Axis x_axis, y_axis, z_axis;
        readonly HashSet<Axis> axes;

        // Camera Parameters
        float D = 800;
        float Theta = -100;
        float Phi = -10;
        float R = 10;
        const float startD = 800;
        const float startTheta = -100;
        const float startPhi = -10;
        const float startR = 10;

        // Animation variables
        bool AnimationForward = true;
        Shape animatedShape;
        private int animatedShapeIndex = 0;

        // Modified Shape
        private Shape selectedShape;
        private int selectedShapeIndex = 0;

        // Cube Parameters
        float angleX = 0.0f;
        float angleY = 0.0f;
        float angleZ = 0.0f;
        int phase = 1;

        // Objects
        readonly Cube cube;
        readonly Pyramid pyramid;
        readonly Dictionary<string, Shape> shapes;

        // Tools
        readonly System.Timers.Timer timer = new();

        // Controls
        private Dictionary<Keys, bool> shapeModifiers = new()
        {
            { Keys.X, false },
            { Keys.Y, false },
            { Keys.Z, false },
            { Keys.S, false },
            { Keys.Left, false },
            { Keys.Right, false },
            { Keys.Up, false },
            { Keys.Down, false },
            { Keys.PageUp, false },
            { Keys.PageDown, false },
        };

        private readonly Dictionary<Keys, bool> cameraModifiers = new()
        {
            { Keys.T, false },
            { Keys.P, false },
            { Keys.R, false },
            { Keys.D, false }
        };

        public Form1()
        {
            InitializeComponent();

            Width = WIDTH;
            Height = HEIGHT;
            DoubleBuffered = true;

            // Timer
            timer.Interval = 50;
            timer.Elapsed += Timer_Elapsed!;
            timer.Start();

            // Define axes
            x_axis = new AxisX(Color.Red, 3);
            y_axis = new AxisY(Color.Green, 3);
            z_axis = new AxisZ(Color.Blue, 3);
            axes = [x_axis, y_axis, z_axis];

            // Create objects
            cube = new Cube(Color.Red, 1);
            pyramid = new Pyramid(Color.Green, 1);

            pyramid.ModelTransformation(new(3, 3, 0), new(5, 5, 5), 1);

            shapes = new()
            {
                { cube.Name.ToLower(), cube },
                { pyramid.Name.ToLower(), pyramid }
            };

            // Animated Shape
            animatedShape = cube;
            selectedShape = cube;
        }

        private void CreateShape(Vector translation, Vector rotation, string type, Color color, float size, string name)
        {
            if (type.Equals("cube", StringComparison.CurrentCultureIgnoreCase))
            {
                Cube c = new(color, size);
                c.ModelTransformation(translation, rotation, 1f);
                c.Rename(name);
                shapes.Add(c.Name, c);
            }
            else if (type.Equals("pyramid", StringComparison.CurrentCultureIgnoreCase))
            {
                Pyramid c = new(color, size);
                c.ModelTransformation(translation, rotation, 1f);
                c.Rename(name);
                shapes.Add(c.Name, c);
            }
        }

        private void Animation1(Shape shape)
        {
            Theta -= 1.0f;

            if (shape.Enlargement >= 1.5f)
                AnimationForward = false;
            else if (shape.Enlargement < 1.0f)
            {
                AnimationForward = true;
                phase = 2;
            }

            if (AnimationForward)
                shape.Enlargement += 0.01f;
            else
                shape.Enlargement -= 0.01f;
        }

        private void Animation2(Shape shape)
        {
            Theta -= 1.0f;

            if (angleX >= Matrix.DegreesToRadians(45))
                AnimationForward = false;
            else if (angleX < 0.0f)
            {
                phase = 3;
                AnimationForward = true;
            }

            if (AnimationForward)
                Rotate(new(Matrix.DegreesToRadians(1), 0.0f, 0.0f), shape);
            else
                Rotate(new(-Matrix.DegreesToRadians(1), 0.0f, 0.0f), shape);
        }

        private void Animation3(Shape shape)
        {
            Phi += 1.0f;
            if (angleY >= Matrix.DegreesToRadians(45))
                AnimationForward = false;
            else if (angleY < 0.0f)
            {
                AnimationForward = true;
                phase = 4;
            }

            if (AnimationForward)
                Rotate(new(0.0f, Matrix.DegreesToRadians(1), 0.0f), shape);
            else
                Rotate(new(0.0f, -Matrix.DegreesToRadians(1), 0.0f), shape);
        }

        private void Animation4()
        {
            if (Theta != startTheta)
            {
                if (Theta < startTheta)
                    Theta += 1.0f;
                else
                    Theta -= 1.0f;
            }
            if (Phi != startPhi)
            {
                if (Phi < startPhi)
                    Phi += 1.0f;
                else
                    Phi -= 1.0f;
            }
            else if (Phi == startPhi && Theta == startTheta)
            {
                phase = 1;
            }
        }

        private void RunAnimation(Shape shape)
        {
            if (shape.IsAnimating)
            {
                if (phase == 1)
                {
                    Animation1(shape);
                }
                if (phase == 2)
                {
                    Animation2(shape);
                }
                if (phase == 3)
                {
                    Animation3(shape);
                }
                if (phase == 4)
                {
                    Animation4();
                }
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RunAnimation(animatedShape);
            Invalidate();
        }

        private List<Vector> Project(Shape shape)
        {
            return Shape.Project(shape.Projection, R, Theta, Phi, D);
        }

        private List<Vector> Viewport(List<Vector> vertexBuffer)
        {
            List<Vector> result = [];
            foreach (Vector vertex in vertexBuffer)
            {
                result.Add(new(vertex.X + CENTER.X, CENTER.Y - vertex.Y, vertex.Z));
            }
            return result;
        }

        private static void Rotate(Vector rotation, Shape shape)
        {
            shape.Rotate(rotation);
        }

        private static List<Vector> Translate(Vector vector, Shape shape)
        {
            return shape.Translate(vector);
        }

        private static void DrawText(PaintEventArgs e)
        {
            Font font = new("Arial", 12, FontStyle.Bold);
            int fromEdge = 250;

            e.Graphics.DrawString("s/S", font, Brushes.Black, fromEdge, 0);
            e.Graphics.DrawString("Left/Right", font, Brushes.Black, fromEdge, 20);
            e.Graphics.DrawString("Up/Down", font, Brushes.Black, fromEdge, 40);
            e.Graphics.DrawString("PgUp/PgDown", font, Brushes.Black, fromEdge, 60);
            e.Graphics.DrawString("x/X", font, Brushes.Black, fromEdge, 80);
            e.Graphics.DrawString("y/Y", font, Brushes.Black, fromEdge, 100);
            e.Graphics.DrawString("z/Z", font, Brushes.Black, fromEdge, 120);
            e.Graphics.DrawString("r/R", font, Brushes.Black, fromEdge, 160);
            e.Graphics.DrawString("d/D", font, Brushes.Black, fromEdge, 180);
            e.Graphics.DrawString("p/P", font, Brushes.Black, fromEdge, 200);
            e.Graphics.DrawString("t/T", font, Brushes.Black, fromEdge, 220);
            e.Graphics.DrawString("A", font, Brushes.Black, fromEdge, 240);
            e.Graphics.DrawString("`", font, Brushes.Black, fromEdge, 260);
            e.Graphics.DrawString("~", font, Brushes.Black, fromEdge, 280);
            e.Graphics.DrawString("+", font, Brushes.Black, fromEdge, 300);
            e.Graphics.DrawString("-", font, Brushes.Black, fromEdge, 320);
        }

        private void UpdateOnScreenValues(PaintEventArgs e, Shape shape)
        {
            Font font = new("Arial", 12, FontStyle.Bold);
            NormalizeRotationAngles(shape);

            e.Graphics.DrawString("Scale: " + MathF.Round(shape.Enlargement, 2), font, Brushes.Black, 0, 0);
            e.Graphics.DrawString("TransX: " + MathF.Round(shape.Translation.X, 1), font, Brushes.Black, 0, 20);
            e.Graphics.DrawString("TransY: " + MathF.Round(shape.Translation.Y, 1), font, Brushes.Black, 0, 40);
            e.Graphics.DrawString("TransZ: " + MathF.Round(shape.Translation.Z, 1), font, Brushes.Black, 0, 60);
            e.Graphics.DrawString("RotX: " + MathF.Round(Matrix.RadiansToDegrees(angleX), 0), font, Brushes.Black, 0, 80);
            e.Graphics.DrawString("RotY: " + MathF.Round(Matrix.RadiansToDegrees(angleY), 0), font, Brushes.Black, 0, 100);
            e.Graphics.DrawString("RotZ: " + MathF.Round(Matrix.RadiansToDegrees(angleZ), 0), font, Brushes.Black, 0, 120);
            e.Graphics.DrawString("R: " + MathF.Round(R, 1), font, Brushes.Black, 0, 160);
            e.Graphics.DrawString("D: " + D, font, Brushes.Black, 0, 180);
            e.Graphics.DrawString("Phi: " + MathF.Round(Phi, 1), font, Brushes.Black, 0, 200);
            e.Graphics.DrawString("Theta: " + MathF.Round(Theta, 1), font, Brushes.Black, 0, 220);
            e.Graphics.DrawString("isRotating: " + shape.IsAnimating, font, Brushes.Black, 0, 240);
            e.Graphics.DrawString("SelectedShape: " + selectedShape.Name, font, Brushes.Black, 0, 260);
            e.Graphics.DrawString("AnimatedShape: " + animatedShape.Name, font, Brushes.Black, 0, 280);
            e.Graphics.DrawString("CreateShape", font, Brushes.Black, 0, 300);
            e.Graphics.DrawString("RemoveShape", font, Brushes.Black, 0, 320);
            e.Graphics.DrawString("Phase: " + phase, font, Brushes.Black, 0, 340);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // draw info
            DrawText(e);
            UpdateOnScreenValues(e, selectedShape);

            // Draw axes
            foreach (Axis axis in axes)
            {
                axis.Draw(e.Graphics, Viewport(Project(axis)));
            }

            // Draw shapes
            foreach (Shape shape in shapes.Values)
            {
                shape.Draw(e.Graphics, Viewport(Project(shape)));
            }
        }

        private void NormalizeRotationAngles(Shape shape)
        {
            var x = shape.Rotation.X;
            var y = shape.Rotation.Y;
            var z = shape.Rotation.Z;
            angleX = MathF.Atan2(MathF.Sin(x), MathF.Cos(x));
            angleY = MathF.Atan2(MathF.Sin(y), MathF.Cos(y));
            angleZ = MathF.Atan2(MathF.Sin(z), MathF.Cos(z));
        }

        private void ShapeControls(KeyEventArgs e)
        {
            foreach (var keyPair in shapeModifiers)
            {
                Keys key = keyPair.Key;
                var pressed = keyPair.Value;

                if (pressed)
                {
                    if (key == Keys.PageDown)
                        Translate(new Vector(0.0f, 0.0f, -0.1f, 1.0f), selectedShape);

                    if (key == Keys.PageUp)
                        Translate(new Vector(0.0f, 0.0f, 0.1f, 1.0f), selectedShape);

                    if (key == Keys.Left)
                        Translate(new Vector(-0.1f, 0.0f, 0.0f, 1.0f), selectedShape);

                    if (key == Keys.Right)
                        Translate(new Vector(0.1f, 0.0f, 0.0f, 1.0f), selectedShape);

                    if (key == Keys.Up)
                        Translate(new Vector(0.0f, 0.1f, 0.0f, 1.0f), selectedShape);

                    if (key == Keys.Down)
                        Translate(new Vector(0.0f, -0.1f, 0.0f, 1.0f), selectedShape);

                    if (key == Keys.Z)
                    {
                        if (e.Modifiers == Keys.Shift)
                            Rotate(new(0.0f, 0.0f, -Matrix.DegreesToRadians(1)), selectedShape);
                        else
                            Rotate(new(0.0f, 0.0f, Matrix.DegreesToRadians(1)), selectedShape);
                    }

                    if (key == Keys.Y)
                    {
                        if (e.Modifiers == Keys.Shift)
                            Rotate(new(0.0f, -Matrix.DegreesToRadians(1), 0.0f), selectedShape);
                        else
                            Rotate(new(0.0f, Matrix.DegreesToRadians(1), 0.0f), selectedShape);
                    }

                    if (key == Keys.X)
                    {
                        if (e.Modifiers == Keys.Shift)
                            Rotate(new(-Matrix.DegreesToRadians(1), 0.0f, 0.0f), selectedShape);
                        else
                            Rotate(new(Matrix.DegreesToRadians(1), 0.0f, 0.0f), selectedShape);
                    }

                    if (key == Keys.S)
                    {
                        if (e.Modifiers == Keys.Shift)
                            selectedShape.Enlargement += 0.1f;
                        else
                            selectedShape.Enlargement -= 0.1f;
                    }
                }
            }
        }

        private void CameraControls(KeyEventArgs e)
        {
            foreach (var keyPair in cameraModifiers)
            {
                bool pressed = keyPair.Value;
                Keys key = keyPair.Key;

                if (pressed)
                {
                    if (key == Keys.R)
                    {
                        if (e.Modifiers == Keys.Shift)
                            R += 1.0f;
                        else
                            R -= 1.0f;
                    }

                    if (key == Keys.D)
                    {
                        if (e.Modifiers == Keys.Shift)
                            D += 1.0f;
                        else
                            D -= 1.0f;
                    }

                    if (key == Keys.T)
                    {
                        if (e.Modifiers == Keys.Shift)
                            Theta += 1.0f;
                        else
                            Theta -= 1.0f;
                    }

                    if (key == Keys.P)
                    {
                        if (e.Modifiers == Keys.Shift)
                            Phi += 1.0f;
                        else
                            Phi -= 1.0f;
                    }
                }
            }
        }

        private void ExtraControls(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();

            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Shift)
                animatedShape.ToggleAnimation();

            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Shift)
                Reset();

            if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
                CreateShape(this, e);

            if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
                RemoveShape(this, e);

            if (e.KeyCode == Keys.Oem3)
                if (e.Modifiers  == Keys.Shift)
                    CycleSelectedShape("anim");
                else
                    CycleSelectedShape("selec");
        }

        private void Reset()
        {
            AnimationForward = true;
            phase = 1;
            D = startD;
            Theta = startTheta;
            Phi = startPhi;
            R = startR;

            foreach (Axis axis in axes)
                axis.Reset();

            foreach (Shape shape in shapes.Values)
                shape.Reset();

            shapeModifiers = shapeModifiers.ToDictionary(pair => pair.Key, pair => false);
            Invalidate();
        }

        private void CycleSelectedShape(string sender)
        {
            try
            {
                if (sender == "selec")
                {
                    selectedShapeIndex++;
                    if (selectedShapeIndex >= shapes.Count)
                        selectedShapeIndex = 0;

                    selectedShape = shapes.ElementAt(selectedShapeIndex).Value;

                }
                else if (sender == "anim")
                {
                    animatedShapeIndex++;
                    if (animatedShapeIndex >= shapes.Count)
                        animatedShapeIndex = 0;

                    animatedShape = shapes.ElementAt(animatedShapeIndex).Value;
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                MessageBox.Show($"No more shapes in list: {e.Message}");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ExtraControls(e);

            if (shapeModifiers.ContainsKey(e.KeyCode))
            {
                shapeModifiers[e.KeyCode] = true;
                ShapeControls(e);
            }

            if (cameraModifiers.ContainsKey(e.KeyCode))
            {
                cameraModifiers[e.KeyCode] = true;
                CameraControls(e);
            }

            Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (shapeModifiers.ContainsKey(e.KeyCode))
            {
                shapeModifiers[e.KeyCode] = false;
                ShapeControls(e);
            }
            if (cameraModifiers.ContainsKey(e.KeyCode))
            {
                cameraModifiers[e.KeyCode] = false;
                CameraControls(e);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CENTER.X = this.Width * 0.5f;
            CENTER.Y = this.Height * 0.5f;
            Invalidate();
        }

        private void CreateShape(object sender, EventArgs e)
        {
            Tuple<Vector, Vector, string, Color, float, string> shape = new(new(), new(), string.Empty, Color.Black, 1f, string.Empty);

            if (ShapeCreationBox("Create Shape", ref shape) == DialogResult.OK)
            {
                CreateShape(shape.Item1, shape.Item2, shape.Item3, shape.Item4, shape.Item5, shape.Item6);
            }
        }

        private void RemoveShape(object sender, EventArgs e)
        {
            string shapeName = string.Empty;
            ShapeRemovalBox(ref shapeName);

            if (!shapes.ContainsKey(shapeName))
            {
                MessageBox.Show($"No shape with that name exists");
                return;
            }
            else
                shapes.Remove(shapeName);
        }

        private static DialogResult ShapeRemovalBox(ref string shapeName)
        {
            #region FormInitialisation
            Form form = new();
            Label shapeNameLabel = new();
            TextBox shapeNameTextBox = new();

            Button buttonOK = new();
            Button buttonCancel = new();

            form.Text = "Remove Shape";
            shapeNameLabel.Text = "Name of shape to be removed";

            buttonOK.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOK.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            form.ClientSize = new Size(400, 200);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            shapeNameLabel.SetBounds(35, 35, 100, 20);
            shapeNameTextBox.SetBounds(35, 55, 100, 20);

            buttonOK.SetBounds(35, 150, 95, 40);
            buttonCancel.SetBounds(270, 150, 95, 40);


            form.Controls.AddRange([
                shapeNameLabel, shapeNameTextBox,
                buttonOK, buttonCancel
                ]);
            form.AcceptButton = buttonOK;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            #endregion

            shapeName = shapeNameTextBox.Text.ToLower();
            return dialogResult;
        }

        private static DialogResult ShapeCreationBox(string title, ref Tuple<Vector, Vector, string, Color, float, string> shape)
        {
            #region FormInitialisation
            Form form = new();
            Label rotationLabel = new();
            Label rotationXLabel = new();
            Label rotationYLabel = new();
            Label rotationZLabel = new();

            Label translationLabel = new();
            Label translationXLabel = new();
            Label translationYLabel = new();
            Label translationZLabel = new();

            Label shapeTypeLabel = new();
            Label colorLabel = new();
            Label sizeLabel = new();
            Label nameLabel = new();

            TextBox translationXBox = new();
            TextBox translationYBox = new();
            TextBox translationZBox = new();

            TextBox rotationXBox = new();
            TextBox rotationYBox = new();
            TextBox rotationZBox = new();

            TextBox sizeBox = new();
            TextBox nameBox = new();

            Button buttonOK = new();
            Button buttonCancel = new();

            ComboBox colorList = new()
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            List<Color> colors = GetAllColors();
            for (int i = 0; i<colors.Count; i++)
            {
                colorList.Items.Add(colors[i].Name);
            }

            ComboBox shapeList = new()
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            List<string> shapeNames = new(GetShapeTypes());
            for (int i = 0; i < shapeNames.Count; i++)
            {
                shapeList.Items.Add(shapeNames[i]);
            }


            form.Text = title;
            shapeTypeLabel.Text = "Shape Type";

            translationLabel.Text = "Translation";
            translationXLabel.Text = "X";
            translationYLabel.Text = "Y";
            translationZLabel.Text = "Z";

            rotationLabel.Text = "Rotation";
            rotationXLabel.Text = "X";
            rotationYLabel.Text = "Y";
            rotationZLabel.Text = "Z";

            colorLabel.Text = "Color";
            sizeLabel.Text = "Size";
            nameLabel.Text = "Name";

            buttonOK.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOK.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            shapeTypeLabel.AutoSize = true;
            form.ClientSize = new Size(400, 200);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            nameLabel.SetBounds(35, 5, 40, 20);
            nameBox.SetBounds(80, 5, 285, 20);
            shapeTypeLabel.SetBounds(35, 35, 100, 20);
            shapeList.SetBounds(35, 55, 100, 20);

            colorLabel.SetBounds(150, 35, 150, 20);
            colorList.SetBounds(150, 55, 150, 20);

            sizeLabel.SetBounds(315, 35, 50, 20);
            sizeBox.SetBounds(315, 55, 50, 20);

            translationLabel.SetBounds(35, 100, 100, 15);
            translationXLabel.SetBounds(35, 120, 10, 15);
            translationXBox.SetBounds(50, 120, 30, 15);
            translationYLabel.SetBounds(85, 120, 10, 15);
            translationYBox.SetBounds(100, 120, 30, 15);
            translationZLabel.SetBounds(135, 120, 10, 15);
            translationZBox.SetBounds(150, 120, 30, 15);

            rotationLabel.SetBounds(220, 100, 100, 15);
            rotationXLabel.SetBounds(220, 120, 10, 15);
            rotationXBox.SetBounds(235, 120, 30, 15);
            rotationYLabel.SetBounds(270, 120, 10, 15);
            rotationYBox.SetBounds(285, 120, 30, 15);
            rotationZLabel.SetBounds(320, 120, 10, 15);
            rotationZBox.SetBounds(335, 120, 30, 15);

            buttonOK.SetBounds(35, 150, 95, 40);
            buttonCancel.SetBounds(270, 150, 95, 40);


            form.Controls.AddRange([
                shapeTypeLabel, shapeList,
                colorLabel, colorList,
                sizeLabel, sizeBox,
                translationLabel,
                translationXLabel, translationXBox,
                translationYLabel, translationYBox,
                translationZLabel, translationZBox,
                rotationLabel,
                rotationXLabel, rotationXBox,
                rotationYLabel, rotationYBox,
                rotationZLabel, rotationZBox,
                nameLabel, nameBox,
                buttonOK, buttonCancel
                ]);
            form.AcceptButton = buttonOK;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            #endregion

            Vector translation = new();
            Vector rotation = new();
            string shapeType = "cube";
            string shapeName = shapeType;
            float size = 1;
            Color c = Color.White;

            if (!(ValidNumberInput(translationXBox.Text) && ValidNumberInput(translationYBox.Text) && ValidNumberInput(translationZBox.Text)))
                return DialogResult.Cancel;
            translation = new(float.Parse(translationXBox.Text), float.Parse(translationYBox.Text), float.Parse(translationZBox.Text));

            if (!(ValidNumberInput(rotationXBox.Text) && ValidNumberInput(rotationYBox.Text) && ValidNumberInput(rotationZBox.Text)))
                return DialogResult.Cancel;
            rotation = new(float.Parse(rotationXBox.Text), float.Parse(rotationYBox.Text), float.Parse(rotationZBox.Text));

            if (shapeList.SelectedItem is null)
                return DialogResult.Cancel;
            shapeType = shapeList.SelectedItem.ToString()!;

            if (!ValidNumberInput(sizeBox.Text))
                return DialogResult.Cancel;
            size = float.Parse(sizeBox.Text);

            if (colorList.SelectedItem is null)
                return DialogResult.Cancel;
            c = colors.Where(col => col.Name == colorList.SelectedItem.ToString()).FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(nameBox.Text))
                shapeName = nameBox.Text.ToLower();

            shape = Tuple.Create(translation, rotation, shapeType, c, size, shapeName);

            return dialogResult;
        }

        private static bool ValidNumberInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            if (!NumberRegex().IsMatch(input))
            {
                MessageBox.Show("Please enter only numbers");
                return false;
            }
            return true;
        }

        private static List<string> GetShapeTypes()
        {
            return ["Cube", "Pyramid"];
        }

        private static List<Color> GetAllColors()
        {
            List<Color> allColors = [];

            foreach (PropertyInfo property in typeof(Color).GetProperties())
            {
                if (property.PropertyType == typeof(Color))
                {
                    allColors.Add((Color)property.GetValue(null)!);
                }
            }

            return allColors;
        }

        [System.Text.RegularExpressions.GeneratedRegex(@"^-?\d*\.?\d+$")]
        private static partial System.Text.RegularExpressions.Regex NumberRegex();
    }
}