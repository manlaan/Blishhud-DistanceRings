using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Blish_HUD.Graphics.UI;
using Blish_HUD.Input;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework.Graphics;

namespace Manlaan.DistanceRings.Views
{
    public class SettingsView : View
    {
        Panel colorPickerPanel;
        ColorPicker colorPicker;
        ColorBox settingDistanceRingsColor1_Box, settingDistanceRingsColor2_Box, settingDistanceRingsColor3_Box, settingDistanceRingsColor4_Box, settingDistanceRingsColor5_Box;
        SettingEntry<string> colorBoxSelected = new SettingEntry<string>();

        protected override void Build(Container buildPanel) {
            Panel parentPanel = new Panel() {
                CanScroll = false,
                Parent = buildPanel,
                Height = buildPanel.Height,
                HeightSizingMode = SizingMode.AutoSize,
                Width = 700,  //bug? with buildPanel.Width changing to 40 after loading a different module settings and coming back.,
            };
            parentPanel.LeftMouseButtonPressed += delegate {
                if (colorPickerPanel.Visible && !colorPickerPanel.MouseOver && !settingDistanceRingsColor1_Box.MouseOver)
                    colorPickerPanel.Visible = false;
            };

            colorPickerPanel = new Panel() {
                Location = new Point(parentPanel.Width - 420 - 10, 10),
                Size = new Point(420, 255),
                Visible = false,
                ZIndex = 10,
                Parent = parentPanel,
                BackgroundTexture = DistanceRingsModule.ModuleInstance.ContentsManager.GetTexture("155976.png"),
                ShowBorder = false,
            };
            Panel colorPickerBG = new Panel() {
                Location = new Point(15, 15),
                Size = new Point(colorPickerPanel.Size.X - 35, colorPickerPanel.Size.Y - 30),
                Parent = colorPickerPanel,
                BackgroundTexture = DistanceRingsModule.ModuleInstance.ContentsManager.GetTexture("buttondark.png"),
                ShowBorder = true,
            };
            colorPicker = new ColorPicker() {
                Location = new Point(10, 10),
                CanScroll = false,
                Size = new Point(colorPickerBG.Size.X - 20, colorPickerBG.Size.Y - 20),
                Parent = colorPickerBG,
                ShowTint = false,
                Visible = true
            };
            colorPicker.SelectedColorChanged += delegate {
                colorPicker.AssociatedColorBox.Color = colorPicker.SelectedColor;
                try {
                    colorBoxSelected.Value = colorPicker.SelectedColor.Name;
                } catch {
                    colorBoxSelected.Value = "White0";
                }
                colorPickerPanel.Visible = false;
            };
            colorPicker.LeftMouseButtonPressed += delegate {
                colorPickerPanel.Visible = false;
            };
            foreach (var color in DistanceRingsModule._colors) {
                colorPicker.Colors.Add(color);
            }

            #region Ring 1
            Checkbox settingDistanceRingsEnable1 = new Checkbox() {
                Location = new Point(10, 18),
                Parent = parentPanel,
                Checked = DistanceRingsModule._settingDistanceRingsEnable1.Value
            };
            settingDistanceRingsEnable1.CheckedChanged += delegate {
                DistanceRingsModule._settingDistanceRingsEnable1.Value = settingDistanceRingsEnable1.Checked;
            };

            Label settingDistanceRingsRadius1_Label = new Label() {
                Location = new Point(40, settingDistanceRingsEnable1.Top - 2),
                Width = 50,
                AutoSizeHeight = false,
                WrapText = false,
                Parent = parentPanel,
                Text = "Radius: ",
            };
            TextBox settingDistanceRingsRadius1_TextBox = new TextBox() {
                Location = new Point(settingDistanceRingsRadius1_Label.Right, settingDistanceRingsEnable1.Top - 6),
                Size = new Point(60, 27),
                Text = DistanceRingsModule._settingDistanceRingsRadius1.Value,
                Parent = parentPanel
            };
            settingDistanceRingsRadius1_TextBox.TextChanged += delegate {
                try {
                    int.Parse(settingDistanceRingsRadius1_TextBox.Text);
                    DistanceRingsModule._settingDistanceRingsRadius1.Value = settingDistanceRingsRadius1_TextBox.Text;
                }
                catch { }
            };
            settingDistanceRingsColor1_Box = new ColorBox() {
                Location = new Point(settingDistanceRingsRadius1_TextBox.Right + 8, settingDistanceRingsEnable1.Top - 8),
                Parent = parentPanel,
                Color = DistanceRingsModule._colors.Find(x => x.Name.Equals(DistanceRingsModule._settingDistanceRingsColor1.Value)),
            };
            settingDistanceRingsColor1_Box.Click += delegate (object sender, MouseEventArgs e) {
                SetColorSetting(ref DistanceRingsModule._settingDistanceRingsColor1);
                colorPicker.AssociatedColorBox = (ColorBox)sender;
                colorPickerPanel.Visible = !colorPickerPanel.Visible;
            };

            Label settingDistanceRingsOpacity1_Label = new Label() {
                Location = new Point(settingDistanceRingsColor1_Box.Right + 8, settingDistanceRingsEnable1.Top - 2),
                Width = 55,
                AutoSizeHeight = false,
                WrapText = false,
                Parent = parentPanel,
                Text = "Opacity: ",
            };
            TrackBar settingDistanceRingsOpacity1_Slider = new TrackBar() {
                Location = new Point(settingDistanceRingsOpacity1_Label.Right + 8, settingDistanceRingsOpacity1_Label.Top),
                Width = 250,
                MaxValue = 100,
                MinValue = 0,
                Value = DistanceRingsModule._settingDistanceRingsOpacity1.Value * 100,
                Parent = parentPanel,
            };
            settingDistanceRingsOpacity1_Slider.ValueChanged += delegate { DistanceRingsModule._settingDistanceRingsOpacity1.Value = settingDistanceRingsOpacity1_Slider.Value / 100; };
            #endregion

            #region Ring 2
            Checkbox settingDistanceRingsEnable2 = new Checkbox() {
                Location = new Point(10, settingDistanceRingsEnable1.Bottom + 14),
                Parent = parentPanel,
                Checked = DistanceRingsModule._settingDistanceRingsEnable2.Value
            };
            settingDistanceRingsEnable2.CheckedChanged += delegate {
                DistanceRingsModule._settingDistanceRingsEnable2.Value = settingDistanceRingsEnable2.Checked;
            };

            Label settingDistanceRingsRadius2_Label = new Label() {
                Location = new Point(40, settingDistanceRingsEnable2.Top - 2),
                Width = 50,
                AutoSizeHeight = false,
                WrapText = false,
                Parent = parentPanel,
                Text = "Radius: ",
            };
            TextBox settingDistanceRingsRadius2_TextBox = new TextBox() {
                Location = new Point(settingDistanceRingsRadius2_Label.Right, settingDistanceRingsEnable2.Top - 6),
                Size = new Point(60, 27),
                Text = DistanceRingsModule._settingDistanceRingsRadius2.Value,
                Parent = parentPanel
            };
            settingDistanceRingsRadius2_TextBox.TextChanged += delegate {
                try {
                    int.Parse(settingDistanceRingsRadius2_TextBox.Text);
                    DistanceRingsModule._settingDistanceRingsRadius2.Value = settingDistanceRingsRadius2_TextBox.Text;
                }
                catch { }
            };
            settingDistanceRingsColor2_Box = new ColorBox() {
                Location = new Point(settingDistanceRingsRadius2_TextBox.Right + 8, settingDistanceRingsEnable2.Top - 8),
                Parent = parentPanel,
                Color = DistanceRingsModule._colors.Find(x => x.Name.Equals(DistanceRingsModule._settingDistanceRingsColor2.Value)),
            };
            settingDistanceRingsColor2_Box.Click += delegate (object sender, MouseEventArgs e) {
                SetColorSetting(ref DistanceRingsModule._settingDistanceRingsColor2);
                colorPicker.AssociatedColorBox = (ColorBox)sender;
                colorPickerPanel.Visible = !colorPickerPanel.Visible;
            };

            Label settingDistanceRingsOpacity2_Label = new Label() {
                Location = new Point(settingDistanceRingsColor2_Box.Right + 8, settingDistanceRingsEnable2.Top - 2),
                Width = 55,
                AutoSizeHeight = false,
                WrapText = false,
                Parent = parentPanel,
                Text = "Opacity: ",
            };
            TrackBar settingDistanceRingsOpacity2_Slider = new TrackBar() {
                Location = new Point(settingDistanceRingsOpacity2_Label.Right + 8, settingDistanceRingsOpacity2_Label.Top),
                Width = 250,
                MaxValue = 100,
                MinValue = 0,
                Value = DistanceRingsModule._settingDistanceRingsOpacity2.Value * 100,
                Parent = parentPanel,
            };
            settingDistanceRingsOpacity2_Slider.ValueChanged += delegate { DistanceRingsModule._settingDistanceRingsOpacity2.Value = settingDistanceRingsOpacity2_Slider.Value / 100; };
            #endregion

            #region Ring 3
            Checkbox settingDistanceRingsEnable3 = new Checkbox() {
                Location = new Point(10, settingDistanceRingsEnable2.Bottom + 14),
                Parent = parentPanel,
                Checked = DistanceRingsModule._settingDistanceRingsEnable3.Value
            };
            settingDistanceRingsEnable3.CheckedChanged += delegate {
                DistanceRingsModule._settingDistanceRingsEnable3.Value = settingDistanceRingsEnable3.Checked;
            };

            Label settingDistanceRingsRadius3_Label = new Label() {
                Location = new Point(40, settingDistanceRingsEnable3.Top - 2),
                Width = 50,
                AutoSizeHeight = false,
                WrapText = false,
                Parent = parentPanel,
                Text = "Radius: ",
            };
            TextBox settingDistanceRingsRadius3_TextBox = new TextBox() {
                Location = new Point(settingDistanceRingsRadius3_Label.Right, settingDistanceRingsEnable3.Top - 6),
                Size = new Point(60, 27),
                Text = DistanceRingsModule._settingDistanceRingsRadius3.Value,
                Parent = parentPanel
            };
            settingDistanceRingsRadius3_TextBox.TextChanged += delegate {
                try {
                    int.Parse(settingDistanceRingsRadius3_TextBox.Text);
                    DistanceRingsModule._settingDistanceRingsRadius3.Value = settingDistanceRingsRadius3_TextBox.Text;
                }
                catch { }
            };
            settingDistanceRingsColor3_Box = new ColorBox() {
                Location = new Point(settingDistanceRingsRadius3_TextBox.Right + 8, settingDistanceRingsEnable3.Top - 8),
                Parent = parentPanel,
                Color = DistanceRingsModule._colors.Find(x => x.Name.Equals(DistanceRingsModule._settingDistanceRingsColor3.Value)),
            };
            settingDistanceRingsColor3_Box.Click += delegate (object sender, MouseEventArgs e) {
                SetColorSetting(ref DistanceRingsModule._settingDistanceRingsColor3);
                colorPicker.AssociatedColorBox = (ColorBox)sender;
                colorPickerPanel.Visible = !colorPickerPanel.Visible;
            };

            Label settingDistanceRingsOpacity3_Label = new Label() {
                Location = new Point(settingDistanceRingsColor3_Box.Right + 8, settingDistanceRingsEnable3.Top - 2),
                Width = 55,
                AutoSizeHeight = false,
                WrapText = false,
                Parent = parentPanel,
                Text = "Opacity: ",
            };
            TrackBar settingDistanceRingsOpacity3_Slider = new TrackBar() {
                Location = new Point(settingDistanceRingsOpacity3_Label.Right + 8, settingDistanceRingsOpacity3_Label.Top),
                Width = 250,
                MaxValue = 100,
                MinValue = 0,
                Value = DistanceRingsModule._settingDistanceRingsOpacity3.Value * 100,
                Parent = parentPanel,
            };
            settingDistanceRingsOpacity3_Slider.ValueChanged += delegate { DistanceRingsModule._settingDistanceRingsOpacity3.Value = settingDistanceRingsOpacity3_Slider.Value / 100; };
            #endregion

            #region Ring 4
            Checkbox settingDistanceRingsEnable4 = new Checkbox() {
                Location = new Point(10, settingDistanceRingsEnable3.Bottom + 14),
                Parent = parentPanel,
                Checked = DistanceRingsModule._settingDistanceRingsEnable4.Value
            };
            settingDistanceRingsEnable4.CheckedChanged += delegate {
                DistanceRingsModule._settingDistanceRingsEnable4.Value = settingDistanceRingsEnable4.Checked;
            };

            Label settingDistanceRingsRadius4_Label = new Label() {
                Location = new Point(40, settingDistanceRingsEnable4.Top - 2),
                Width = 50,
                AutoSizeHeight = false,
                WrapText = false,
                Parent = parentPanel,
                Text = "Radius: ",
            };
            TextBox settingDistanceRingsRadius4_TextBox = new TextBox() {
                Location = new Point(settingDistanceRingsRadius4_Label.Right, settingDistanceRingsEnable4.Top - 6),
                Size = new Point(60, 27),
                Text = DistanceRingsModule._settingDistanceRingsRadius4.Value,
                Parent = parentPanel
            };
            settingDistanceRingsRadius4_TextBox.TextChanged += delegate {
                try {
                    int.Parse(settingDistanceRingsRadius4_TextBox.Text);
                    DistanceRingsModule._settingDistanceRingsRadius4.Value = settingDistanceRingsRadius4_TextBox.Text;
                }
                catch { }
            };
            settingDistanceRingsColor4_Box = new ColorBox() {
                Location = new Point(settingDistanceRingsRadius4_TextBox.Right + 8, settingDistanceRingsEnable4.Top - 8),
                Parent = parentPanel,
                Color = DistanceRingsModule._colors.Find(x => x.Name.Equals(DistanceRingsModule._settingDistanceRingsColor4.Value)),
            };
            settingDistanceRingsColor4_Box.Click += delegate (object sender, MouseEventArgs e) {
                SetColorSetting(ref DistanceRingsModule._settingDistanceRingsColor4);
                colorPicker.AssociatedColorBox = (ColorBox)sender;
                colorPickerPanel.Visible = !colorPickerPanel.Visible;
            };

            Label settingDistanceRingsOpacity4_Label = new Label() {
                Location = new Point(settingDistanceRingsColor4_Box.Right + 8, settingDistanceRingsEnable4.Top - 2),
                Width = 55,
                AutoSizeHeight = false,
                WrapText = false,
                Parent = parentPanel,
                Text = "Opacity: ",
            };
            TrackBar settingDistanceRingsOpacity4_Slider = new TrackBar() {
                Location = new Point(settingDistanceRingsOpacity4_Label.Right + 8, settingDistanceRingsOpacity4_Label.Top),
                Width = 250,
                MaxValue = 100,
                MinValue = 0,
                Value = DistanceRingsModule._settingDistanceRingsOpacity4.Value * 100,
                Parent = parentPanel,
            };
            settingDistanceRingsOpacity4_Slider.ValueChanged += delegate { DistanceRingsModule._settingDistanceRingsOpacity4.Value = settingDistanceRingsOpacity4_Slider.Value / 100; };
            #endregion

            #region Ring 5
            Checkbox settingDistanceRingsEnable5 = new Checkbox() {
                Location = new Point(10, settingDistanceRingsEnable4.Bottom + 14),
                Parent = parentPanel,
                Checked = DistanceRingsModule._settingDistanceRingsEnable5.Value
            };
            settingDistanceRingsEnable5.CheckedChanged += delegate {
                DistanceRingsModule._settingDistanceRingsEnable5.Value = settingDistanceRingsEnable5.Checked;
            };

            Label settingDistanceRingsRadius5_Label = new Label() {
                Location = new Point(40, settingDistanceRingsEnable5.Top - 2),
                Width = 50,
                AutoSizeHeight = false,
                WrapText = false,
                Parent = parentPanel,
                Text = "Radius: ",
            };
            TextBox settingDistanceRingsRadius5_TextBox = new TextBox() {
                Location = new Point(settingDistanceRingsRadius5_Label.Right, settingDistanceRingsEnable5.Top - 6),
                Size = new Point(60, 27),
                Text = DistanceRingsModule._settingDistanceRingsRadius5.Value,
                Parent = parentPanel
            };
            settingDistanceRingsRadius5_TextBox.TextChanged += delegate {
                try {
                    int.Parse(settingDistanceRingsRadius5_TextBox.Text);
                    DistanceRingsModule._settingDistanceRingsRadius5.Value = settingDistanceRingsRadius5_TextBox.Text;
                }
                catch { }
            };
            settingDistanceRingsColor5_Box = new ColorBox() {
                Location = new Point(settingDistanceRingsRadius5_TextBox.Right + 8, settingDistanceRingsEnable5.Top - 8),
                Parent = parentPanel,
                Color = DistanceRingsModule._colors.Find(x => x.Name.Equals(DistanceRingsModule._settingDistanceRingsColor5.Value)),
            };
            settingDistanceRingsColor5_Box.Click += delegate (object sender, MouseEventArgs e) {
                SetColorSetting(ref DistanceRingsModule._settingDistanceRingsColor5);
                colorPicker.AssociatedColorBox = (ColorBox)sender;
                colorPickerPanel.Visible = !colorPickerPanel.Visible;
            };

            Label settingDistanceRingsOpacity5_Label = new Label() {
                Location = new Point(settingDistanceRingsColor5_Box.Right + 8, settingDistanceRingsEnable5.Top - 2),
                Width = 55,
                AutoSizeHeight = false,
                WrapText = false,
                Parent = parentPanel,
                Text = "Opacity: ",
            };
            TrackBar settingDistanceRingsOpacity5_Slider = new TrackBar() {
                Location = new Point(settingDistanceRingsOpacity5_Label.Right + 8, settingDistanceRingsOpacity5_Label.Top),
                Width = 250,
                MaxValue = 100,
                MinValue = 0,
                Value = DistanceRingsModule._settingDistanceRingsOpacity5.Value * 100,
                Parent = parentPanel,
            };
            settingDistanceRingsOpacity5_Slider.ValueChanged += delegate { DistanceRingsModule._settingDistanceRingsOpacity5.Value = settingDistanceRingsOpacity5_Slider.Value / 100; };
            #endregion


            Label settingDistanceRingsVerticalOffset_Label = new Label() {
                Location = new Point(10, settingDistanceRingsEnable5.Bottom + 14),
                Width = 100,
                AutoSizeHeight = false,
                WrapText = false,
                Parent = parentPanel,
                Text = "Vertical Offset: ",
            };
            TrackBar settingDistanceRingsVerticalOffset_Slider = new TrackBar() {
                Location = new Point(settingDistanceRingsVerticalOffset_Label.Right + 8, settingDistanceRingsVerticalOffset_Label.Top),
                Width = 250,
                MaxValue = 20,
                MinValue = 0,
                Value = (DistanceRingsModule._settingDistanceRingsVerticalOffset.Value * 5) + 10,
                Parent = parentPanel,
            };
            settingDistanceRingsVerticalOffset_Slider.ValueChanged += delegate { DistanceRingsModule._settingDistanceRingsVerticalOffset.Value = (settingDistanceRingsVerticalOffset_Slider.Value - 10) / 5; };
        }

        private void SetColorSetting(ref SettingEntry<string> setting) {
            colorBoxSelected = setting;
        }

    }
}
