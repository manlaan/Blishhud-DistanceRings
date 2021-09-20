using Blish_HUD;
using Blish_HUD.Graphics.UI;
using Blish_HUD.Modules;
using Blish_HUD.Modules.Managers;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Collections.Generic;
using Manlaan.DistanceRings.Models;

namespace Manlaan.DistanceRings
{
    [Export(typeof(Blish_HUD.Modules.Module))]
    public class DistanceRingsModule : Blish_HUD.Modules.Module
    {

        private static readonly Logger Logger = Logger.GetLogger<Module>();

        #region Service Managers
        internal SettingsManager SettingsManager => this.ModuleParameters.SettingsManager;
        internal ContentsManager ContentsManager => this.ModuleParameters.ContentsManager;
        internal DirectoriesManager DirectoriesManager => this.ModuleParameters.DirectoriesManager;
        internal Gw2ApiManager Gw2ApiManager => this.ModuleParameters.Gw2ApiManager;
        #endregion

        public enum Colors
        {
            White,
            Black,
            Red,
            Blue,
            Green,
            Cyan,
            Magenta,
            Yellow,
        }

        public static SettingEntry<bool> _settingDistanceRingsEnable1;
        public static SettingEntry<bool> _settingDistanceRingsEnable2;
        public static SettingEntry<bool> _settingDistanceRingsEnable3;
        public static SettingEntry<bool> _settingDistanceRingsEnable4;
        public static SettingEntry<bool> _settingDistanceRingsEnable5;

        public static SettingEntry<string> _settingDistanceRingsRadius1;
        public static SettingEntry<string> _settingDistanceRingsRadius2;
        public static SettingEntry<string> _settingDistanceRingsRadius3;
        public static SettingEntry<string> _settingDistanceRingsRadius4;
        public static SettingEntry<string> _settingDistanceRingsRadius5;

        public static SettingEntry<string> _settingDistanceRingsColor1;
        public static SettingEntry<string> _settingDistanceRingsColor2;
        public static SettingEntry<string> _settingDistanceRingsColor3;
        public static SettingEntry<string> _settingDistanceRingsColor4;
        public static SettingEntry<string> _settingDistanceRingsColor5;

        public static SettingEntry<float> _settingDistanceRingsOpacity1;
        public static SettingEntry<float> _settingDistanceRingsOpacity2;
        public static SettingEntry<float> _settingDistanceRingsOpacity3;
        public static SettingEntry<float> _settingDistanceRingsOpacity4;
        public static SettingEntry<float> _settingDistanceRingsOpacity5;

        public static SettingEntry<float> _settingDistanceRingsVerticalOffset;

        private Control.DrawRing _ring1;
        private Control.DrawRing _ring2;
        private Control.DrawRing _ring3;
        private Control.DrawRing _ring4;
        private Control.DrawRing _ring5;

        private Texture2D _texturethick;
        private Texture2D _texturethin;
        public static List<Gw2Sharp.WebApi.V2.Models.Color> _colors = new List<Gw2Sharp.WebApi.V2.Models.Color>();


        [ImportingConstructor]
        public DistanceRingsModule([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters) { }

        protected override void DefineSettings(SettingCollection settings)
        {
            _settingDistanceRingsEnable1 = settings.DefineSetting("DistanceRingsEnable1", true, "1. Enabled", "");
            _settingDistanceRingsRadius1 = settings.DefineSetting("DistanceRingsRadius1", "60", "1. Radius", "Radius of the distance ring.");
            _settingDistanceRingsColor1 = settings.DefineSetting("DistanceRingsColor1a", "White0", "1. Color", "Color of distance ring.");
            _settingDistanceRingsOpacity1 = settings.DefineSetting("DistanceRingOpacity1", 1f, "1. Opacity", "Transparency of distance ring.");
            _settingDistanceRingsOpacity1.SetRange(0f, 1f);

            _settingDistanceRingsEnable2 = settings.DefineSetting("DistanceRingsEnable2", false, "2. Enabled", "");
            _settingDistanceRingsRadius2 = settings.DefineSetting("DistanceRingsRadius2", "90", "2. Radius", "Radius of the distance ring.");
            _settingDistanceRingsColor2 = settings.DefineSetting("DistanceRingsColor2a", "White0", "2. Color", "Color of distance ring.");
            _settingDistanceRingsOpacity2 = settings.DefineSetting("DistanceRingOpacity2", 1f, "2. Opacity", "Transparency of distance ring.");
            _settingDistanceRingsOpacity2.SetRange(0f, 1f);

            _settingDistanceRingsEnable3 = settings.DefineSetting("DistanceRingsEnable3", false, "3. Enabled", "");
            _settingDistanceRingsRadius3 = settings.DefineSetting("DistanceRingsRadius3", "120", "3. Radius", "Radius of the distance ring.");
            _settingDistanceRingsColor3 = settings.DefineSetting("DistanceRingsColor3a", "White0", "3. Color", "Color of distance ring.");
            _settingDistanceRingsOpacity3 = settings.DefineSetting("DistanceRingOpacity3", 1f, "3. Opacity", "Transparency of distance ring.");
            _settingDistanceRingsOpacity3.SetRange(0f, 1f);

            _settingDistanceRingsEnable4 = settings.DefineSetting("DistanceRingsEnable4", false, "4. Enabled", "");
            _settingDistanceRingsRadius4 = settings.DefineSetting("DistanceRingsRadius4", "180", "4. Radius", "Radius of the distance ring.");
            _settingDistanceRingsColor4 = settings.DefineSetting("DistanceRingsColor4a", "White0", "4. Color", "Color of distance ring.");
            _settingDistanceRingsOpacity4 = settings.DefineSetting("DistanceRingOpacity4", 1f, "4. Opacity", "Transparency of distance ring.");
            _settingDistanceRingsOpacity4.SetRange(0f, 1f);

            _settingDistanceRingsEnable5 = settings.DefineSetting("DistanceRingsEnable5", false, "5. Enabled", "");
            _settingDistanceRingsRadius5 = settings.DefineSetting("DistanceRingsRadius5", "1200", "5. Radius", "Radius of the distance ring.");
            _settingDistanceRingsColor5 = settings.DefineSetting("DistanceRingsColor5a", "White0", "5. Color", "Color of distance ring.");
            _settingDistanceRingsOpacity5 = settings.DefineSetting("DistanceRingOpacity5", 1f, "5. Opacity", "Transparency of distance ring.");
            _settingDistanceRingsOpacity5.SetRange(0f, 1f);

            _settingDistanceRingsVerticalOffset = settings.DefineSetting("DistanceRingVerticalOffseta", 0f, "Vertical Offset", "How high to offset the distance rings off the ground.");
            //_settingDistanceRingsVerticalOffset.SetRange(-1f, 5f);  //Would prefer to have begining value below 0, but causes mouse drag to be off in settings panel
            //_settingDistanceRingsVerticalOffset.SetRange(0f, 6f);  

            _settingDistanceRingsEnable1.SettingChanged += UpdateSettings_Enabled;
            _settingDistanceRingsEnable2.SettingChanged += UpdateSettings_Enabled;
            _settingDistanceRingsEnable3.SettingChanged += UpdateSettings_Enabled;
            _settingDistanceRingsEnable4.SettingChanged += UpdateSettings_Enabled;
            _settingDistanceRingsEnable5.SettingChanged += UpdateSettings_Enabled;
            _settingDistanceRingsRadius1.SettingChanged += UpdateSettings_Radius;
            _settingDistanceRingsRadius2.SettingChanged += UpdateSettings_Radius;
            _settingDistanceRingsRadius3.SettingChanged += UpdateSettings_Radius;
            _settingDistanceRingsRadius4.SettingChanged += UpdateSettings_Radius;
            _settingDistanceRingsRadius5.SettingChanged += UpdateSettings_Radius;
            _settingDistanceRingsColor1.SettingChanged += UpdateSettings_Color;
            _settingDistanceRingsColor2.SettingChanged += UpdateSettings_Color;
            _settingDistanceRingsColor3.SettingChanged += UpdateSettings_Color;
            _settingDistanceRingsColor4.SettingChanged += UpdateSettings_Color;
            _settingDistanceRingsColor5.SettingChanged += UpdateSettings_Color;
            _settingDistanceRingsOpacity1.SettingChanged += UpdateSettings_Opacity;
            _settingDistanceRingsOpacity2.SettingChanged += UpdateSettings_Opacity;
            _settingDistanceRingsOpacity3.SettingChanged += UpdateSettings_Opacity;
            _settingDistanceRingsOpacity4.SettingChanged += UpdateSettings_Opacity;
            _settingDistanceRingsOpacity5.SettingChanged += UpdateSettings_Opacity;
            _settingDistanceRingsVerticalOffset.SettingChanged += UpdateSettings_VerticalOffset;
        }
        public override IView GetSettingsView() {
            return new DistanceRings.Views.SettingsView();
            //return new SettingsView( (this.ModuleParameters.SettingsManager.ModuleSettings);
        }

        private float DistToPx(float f)
        {
            return (float)(f * 2 * .0128);
        }
        protected override void Initialize()
        {
            _colors = new List<Gw2Sharp.WebApi.V2.Models.Color>();
            foreach (KeyValuePair<string, int[]> color in MyColors.Colors) {
                _colors.Add(new Gw2Sharp.WebApi.V2.Models.Color() { Name = color.Key, Cloth = new Gw2Sharp.WebApi.V2.Models.ColorMaterial() { Rgb = color.Value } });
            }

            _texturethin = ContentsManager.GetTexture("circlethin.png");
            _texturethick = ContentsManager.GetTexture("circlethick.png");

            _ring1 = new Control.DrawRing();
            _ring1.RingTexture = _texturethin;
            GameService.Graphics.World.AddEntity(_ring1);

            _ring2 = new Control.DrawRing();
            _ring2.RingTexture = _texturethin;
            GameService.Graphics.World.AddEntity(_ring2);

            _ring3 = new Control.DrawRing();
            _ring3.RingTexture = _texturethin;
            GameService.Graphics.World.AddEntity(_ring3);

            _ring4 = new Control.DrawRing();
            _ring4.RingTexture = _texturethin;
            GameService.Graphics.World.AddEntity(_ring4);

            _ring5 = new Control.DrawRing();
            _ring5.RingTexture = _texturethin;
            GameService.Graphics.World.AddEntity(_ring5);

            UpdateSettings_Enabled();
            UpdateSettings_Radius();
            UpdateSettings_Color();
            UpdateSettings_Opacity();
            UpdateSettings_VerticalOffset();
        }

        private void UpdateSettings_Enabled(object sender = null, ValueChangedEventArgs<bool> e = null)
        {
            _ring1.RingVisible = _settingDistanceRingsEnable1.Value;
            _ring2.RingVisible = _settingDistanceRingsEnable2.Value;
            _ring3.RingVisible = _settingDistanceRingsEnable3.Value;
            _ring4.RingVisible = _settingDistanceRingsEnable4.Value;
            _ring5.RingVisible = _settingDistanceRingsEnable5.Value;
        }

        private void UpdateSettings_VerticalOffset(object sender = null, ValueChangedEventArgs<float> e = null)
        {
            _ring1.VerticalOffset = _settingDistanceRingsVerticalOffset.Value;
            _ring2.VerticalOffset = _settingDistanceRingsVerticalOffset.Value;
            _ring3.VerticalOffset = _settingDistanceRingsVerticalOffset.Value;
            _ring4.VerticalOffset = _settingDistanceRingsVerticalOffset.Value;
            _ring5.VerticalOffset = _settingDistanceRingsVerticalOffset.Value;
        }

        private void UpdateSettings_Radius(object sender = null, ValueChangedEventArgs<string> e = null)
        {
            float diam = 0;
            diam = DistToPx(float.Parse(_settingDistanceRingsRadius1.Value));
            _ring1.Size = new Vector3(diam, diam, 0);
            _ring1.RingTexture = (float.Parse(_settingDistanceRingsRadius1.Value) <= 70 ? _texturethick : _texturethin);
            diam = DistToPx(float.Parse(_settingDistanceRingsRadius2.Value));
            _ring2.Size = new Vector3(diam, diam, 0);
            _ring2.RingTexture = (float.Parse(_settingDistanceRingsRadius2.Value) <= 70 ? _texturethick : _texturethin);
            diam = DistToPx(float.Parse(_settingDistanceRingsRadius3.Value));
            _ring3.Size = new Vector3(diam, diam, 0);
            _ring3.RingTexture = (float.Parse(_settingDistanceRingsRadius3.Value) <= 70 ? _texturethick : _texturethin);
            diam = DistToPx(float.Parse(_settingDistanceRingsRadius4.Value));
            _ring4.Size = new Vector3(diam, diam, 0);
            _ring4.RingTexture = (float.Parse(_settingDistanceRingsRadius4.Value) <= 70 ? _texturethick : _texturethin);
            diam = DistToPx(float.Parse(_settingDistanceRingsRadius5.Value));
            _ring5.Size = new Vector3(diam, diam, 0);
            _ring5.RingTexture = (float.Parse(_settingDistanceRingsRadius5.Value) <= 70 ? _texturethick : _texturethin);

            _ring1.UpdateRings();
            _ring2.UpdateRings();
            _ring3.UpdateRings();
            _ring4.UpdateRings();
            _ring5.UpdateRings();
        }
        private void UpdateSettings_Color(object sender = null, ValueChangedEventArgs<string> e = null)
        {
            _ring1.RingColor = ToRGB(_colors.Find(x => x.Name.Equals(_settingDistanceRingsColor1.Value))); 
            _ring2.RingColor = ToRGB(_colors.Find(x => x.Name.Equals(_settingDistanceRingsColor2.Value)));
            _ring3.RingColor = ToRGB(_colors.Find(x => x.Name.Equals(_settingDistanceRingsColor3.Value)));
            _ring4.RingColor = ToRGB(_colors.Find(x => x.Name.Equals(_settingDistanceRingsColor4.Value)));
            _ring5.RingColor = ToRGB(_colors.Find(x => x.Name.Equals(_settingDistanceRingsColor5.Value)));

            _ring1.UpdateRings();
            _ring2.UpdateRings();
            _ring3.UpdateRings();
            _ring4.UpdateRings();
            _ring5.UpdateRings();
        }
        private void UpdateSettings_Opacity(object sender = null, ValueChangedEventArgs<float> e = null)
        {
            _ring1.RingOpacity = _settingDistanceRingsOpacity1.Value;
            _ring2.RingOpacity = _settingDistanceRingsOpacity2.Value;
            _ring3.RingOpacity = _settingDistanceRingsOpacity3.Value;
            _ring4.RingOpacity = _settingDistanceRingsOpacity4.Value;
            _ring5.RingOpacity = _settingDistanceRingsOpacity5.Value;
        }

        protected override async Task LoadAsync()
        {
        }

        protected override void OnModuleLoaded(EventArgs e)
        {
            base.OnModuleLoaded(e);
        }

        protected override void Update(GameTime gameTime)
        {
        }

        /// <inheritdoc />
        protected override void Unload()
        {
            _settingDistanceRingsEnable1.SettingChanged -= UpdateSettings_Enabled;
            _settingDistanceRingsEnable2.SettingChanged -= UpdateSettings_Enabled;
            _settingDistanceRingsEnable3.SettingChanged -= UpdateSettings_Enabled;
            _settingDistanceRingsEnable4.SettingChanged -= UpdateSettings_Enabled;
            _settingDistanceRingsEnable5.SettingChanged -= UpdateSettings_Enabled;
            _settingDistanceRingsRadius1.SettingChanged -= UpdateSettings_Radius;
            _settingDistanceRingsRadius2.SettingChanged -= UpdateSettings_Radius;
            _settingDistanceRingsRadius3.SettingChanged -= UpdateSettings_Radius;
            _settingDistanceRingsRadius4.SettingChanged -= UpdateSettings_Radius;
            _settingDistanceRingsRadius5.SettingChanged -= UpdateSettings_Radius;
            _settingDistanceRingsColor1.SettingChanged -= UpdateSettings_Color;
            _settingDistanceRingsColor2.SettingChanged -= UpdateSettings_Color;
            _settingDistanceRingsColor3.SettingChanged -= UpdateSettings_Color;
            _settingDistanceRingsColor4.SettingChanged -= UpdateSettings_Color;
            _settingDistanceRingsColor5.SettingChanged -= UpdateSettings_Color;
            _settingDistanceRingsOpacity1.SettingChanged -= UpdateSettings_Opacity;
            _settingDistanceRingsOpacity2.SettingChanged -= UpdateSettings_Opacity;
            _settingDistanceRingsOpacity3.SettingChanged -= UpdateSettings_Opacity;
            _settingDistanceRingsOpacity4.SettingChanged -= UpdateSettings_Opacity;
            _settingDistanceRingsOpacity5.SettingChanged -= UpdateSettings_Opacity;
            _settingDistanceRingsVerticalOffset.SettingChanged -= UpdateSettings_VerticalOffset;

            GameService.Graphics.World.RemoveEntity(_ring1);
            GameService.Graphics.World.RemoveEntity(_ring2);
            GameService.Graphics.World.RemoveEntity(_ring3);
            GameService.Graphics.World.RemoveEntity(_ring4);
            GameService.Graphics.World.RemoveEntity(_ring5);
        }

        private Color ToRGB(Gw2Sharp.WebApi.V2.Models.Color color) {
            if (color == null)
                return new Color(255, 255, 255);
            else
                return new Color(color.Cloth.Rgb[0], color.Cloth.Rgb[1], color.Cloth.Rgb[2]);
        }

    }

}
