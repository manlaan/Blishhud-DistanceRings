using Blish_HUD;
using Blish_HUD.Modules;
using Blish_HUD.Modules.Managers;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace DistanceRings
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

        private SettingEntry<bool> _settingDistanceRingsEnable1;
        private SettingEntry<bool> _settingDistanceRingsEnable2;
        private SettingEntry<bool> _settingDistanceRingsEnable3;
        private SettingEntry<bool> _settingDistanceRingsEnable4;
        private SettingEntry<bool> _settingDistanceRingsEnable5;

        private SettingEntry<string> _settingDistanceRingsRadius1;
        private SettingEntry<string> _settingDistanceRingsRadius2;
        private SettingEntry<string> _settingDistanceRingsRadius3;
        private SettingEntry<string> _settingDistanceRingsRadius4;
        private SettingEntry<string> _settingDistanceRingsRadius5;

        private SettingEntry<Colors> _settingDistanceRingsColor1;
        private SettingEntry<Colors> _settingDistanceRingsColor2;
        private SettingEntry<Colors> _settingDistanceRingsColor3;
        private SettingEntry<Colors> _settingDistanceRingsColor4;
        private SettingEntry<Colors> _settingDistanceRingsColor5;

        private SettingEntry<float> _settingDistanceRingsOpacity1;
        private SettingEntry<float> _settingDistanceRingsOpacity2;
        private SettingEntry<float> _settingDistanceRingsOpacity3;
        private SettingEntry<float> _settingDistanceRingsOpacity4;
        private SettingEntry<float> _settingDistanceRingsOpacity5;

        private SettingEntry<float> _settingDistanceRingsVerticalOffset;

        private DrawRing _ring1;
        private DrawRing _ring2;
        private DrawRing _ring3;
        private DrawRing _ring4;
        private DrawRing _ring5;

        private Texture2D _texturethick;
        private Texture2D _texturethin;

        [ImportingConstructor]
        public DistanceRingsModule([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters) { }

        protected override void DefineSettings(SettingCollection settings)
        {
            _settingDistanceRingsEnable1 = settings.DefineSetting("DistanceRingsEnable1", true, "1. Enabled", "");
            _settingDistanceRingsRadius1 = settings.DefineSetting("DistanceRingsRadius1", "60", "1. Radius", "Radius of the distance ring.");
            _settingDistanceRingsColor1 = settings.DefineSetting("DistanceRingsColor1", Colors.White, "1. Color", "Color of distance ring.");
            _settingDistanceRingsOpacity1 = settings.DefineSetting("DistanceRingOpacity1", 100f, "1. Opacity", "Transparency of distance ring.");

            _settingDistanceRingsEnable2 = settings.DefineSetting("DistanceRingsEnable2", false, "2. Enabled", "");
            _settingDistanceRingsRadius2 = settings.DefineSetting("DistanceRingsRadius2", "90", "2. Radius", "Radius of the distance ring.");
            _settingDistanceRingsColor2 = settings.DefineSetting("DistanceRingsColor2", Colors.White, "2. Color", "Color of distance ring.");
            _settingDistanceRingsOpacity2 = settings.DefineSetting("DistanceRingOpacity2", 100f, "2. Opacity", "Transparency of distance ring.");

            _settingDistanceRingsEnable3 = settings.DefineSetting("DistanceRingsEnable3", false, "3. Enabled", "");
            _settingDistanceRingsRadius3 = settings.DefineSetting("DistanceRingsRadius3", "120", "3. Radius", "Radius of the distance ring.");
            _settingDistanceRingsColor3 = settings.DefineSetting("DistanceRingsColor3", Colors.White, "3. Color", "Color of distance ring.");
            _settingDistanceRingsOpacity3 = settings.DefineSetting("DistanceRingOpacity3", 100f, "3. Opacity", "Transparency of distance ring.");

            _settingDistanceRingsEnable4 = settings.DefineSetting("DistanceRingsEnable4", false, "4. Enabled", "");
            _settingDistanceRingsRadius4 = settings.DefineSetting("DistanceRingsRadius4", "180", "4. Radius", "Radius of the distance ring.");
            _settingDistanceRingsColor4 = settings.DefineSetting("DistanceRingsColor4", Colors.White, "4. Color", "Color of distance ring.");
            _settingDistanceRingsOpacity4 = settings.DefineSetting("DistanceRingOpacity4", 100f, "4. Opacity", "Transparency of distance ring.");

            _settingDistanceRingsEnable5 = settings.DefineSetting("DistanceRingsEnable5", false, "5. Enabled", "");
            _settingDistanceRingsRadius5 = settings.DefineSetting("DistanceRingsRadius5", "1200", "5. Radius", "Radius of the distance ring.");
            _settingDistanceRingsColor5 = settings.DefineSetting("DistanceRingsColor5", Colors.White, "5. Color", "Color of distance ring.");
            _settingDistanceRingsOpacity5 = settings.DefineSetting("DistanceRingOpacity5", 100f, "5. Opacity", "Transparency of distance ring.");

            _settingDistanceRingsVerticalOffset = settings.DefineSetting("DistanceRingVerticalOffset", 20f, "Vertical Offset", "How high to offset the distance rings off the ground.");

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

        private float DistToPx(float f)
        {
            return (float)(f * 2 * .0128);
        }
        protected override void Initialize()
        {
            _texturethin = ContentsManager.GetTexture("circlethin.png");
            _texturethick = ContentsManager.GetTexture("circlethick.png");

            _ring1 = new DrawRing();
            _ring1.RingTexture = _texturethin;
            _ring1.Size = new Vector3(10, 10, 0);
            _ring1.RingOpacity = 1f;
            _ring1.RingColor = Color.White;
            _ring1.RingVisible = false;
            GameService.Graphics.World.Entities.Add(_ring1);

            _ring2 = new DrawRing();
            _ring2.RingTexture = _texturethin;
            _ring2.Size = new Vector3(0, 0, 0);
            _ring2.RingOpacity = 1f;
            _ring2.RingColor = Color.White;
            _ring2.RingVisible = false;
            GameService.Graphics.World.Entities.Add(_ring2);

            _ring3 = new DrawRing();
            _ring3.RingTexture = _texturethin;
            _ring3.Size = new Vector3(0, 0, 0);
            _ring3.RingOpacity = 1f;
            _ring3.RingColor = Color.White;
            _ring3.RingVisible = false;
            GameService.Graphics.World.Entities.Add(_ring3);

            _ring4 = new DrawRing();
            _ring4.RingTexture = _texturethin;
            _ring4.Size = new Vector3(0, 0, 0);
            _ring4.RingOpacity = 1f;
            _ring4.RingColor = Color.White;
            _ring4.RingVisible = false;
            GameService.Graphics.World.Entities.Add(_ring4);

            _ring5 = new DrawRing();
            _ring5.RingTexture = _texturethin;
            _ring5.Size = new Vector3(0, 0, 0);
            _ring5.RingOpacity = 1f;
            _ring5.RingColor = Color.White;
            _ring5.RingVisible = false;
            GameService.Graphics.World.Entities.Add(_ring5);

            UpdateSettings_Enabled();
            UpdateSettings_Radius();
            UpdateSettings_Color();
            UpdateSettings_Opacity();
            UpdateSettings_VerticalOffset();
        }

        private void UpdateSettings_Enabled(object sender = null, ValueChangedEventArgs<bool> e = null)
        {
            _ring1.Visible = _settingDistanceRingsEnable1.Value;
            _ring2.Visible = _settingDistanceRingsEnable2.Value;
            _ring3.Visible = _settingDistanceRingsEnable3.Value;
            _ring4.Visible = _settingDistanceRingsEnable4.Value;
            _ring5.Visible = _settingDistanceRingsEnable5.Value;
        }

        private void UpdateSettings_VerticalOffset(object sender = null, ValueChangedEventArgs<float> e = null)
        {
            _ring1.VerticalOffset = (_settingDistanceRingsVerticalOffset.Value - 20) / 20;
            _ring2.VerticalOffset = (_settingDistanceRingsVerticalOffset.Value - 20) / 20;
            _ring3.VerticalOffset = (_settingDistanceRingsVerticalOffset.Value - 20) / 20;
            _ring4.VerticalOffset = (_settingDistanceRingsVerticalOffset.Value - 20) / 20;
            _ring5.VerticalOffset = (_settingDistanceRingsVerticalOffset.Value - 20) / 20;
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
        private void UpdateSettings_Color(object sender = null, ValueChangedEventArgs<Colors> e = null)
        {
            _ring1.RingColor = getColor(_settingDistanceRingsColor1.Value);
            _ring2.RingColor = getColor(_settingDistanceRingsColor2.Value);
            _ring3.RingColor = getColor(_settingDistanceRingsColor3.Value);
            _ring4.RingColor = getColor(_settingDistanceRingsColor4.Value);
            _ring5.RingColor = getColor(_settingDistanceRingsColor5.Value);

            _ring1.UpdateRings();
            _ring2.UpdateRings();
            _ring3.UpdateRings();
            _ring4.UpdateRings();
            _ring5.UpdateRings();
        }
        private void UpdateSettings_Opacity(object sender = null, ValueChangedEventArgs<float> e = null)
        {
            _ring1.RingOpacity = _settingDistanceRingsOpacity1.Value / 100;
            _ring2.RingOpacity = _settingDistanceRingsOpacity2.Value / 100;
            _ring3.RingOpacity = _settingDistanceRingsOpacity3.Value / 100;
            _ring4.RingOpacity = _settingDistanceRingsOpacity4.Value / 100;
            _ring5.RingOpacity = _settingDistanceRingsOpacity5.Value / 100;
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

            GameService.Graphics.World.Entities.Remove(_ring1);
            GameService.Graphics.World.Entities.Remove(_ring2);
            GameService.Graphics.World.Entities.Remove(_ring3);
            GameService.Graphics.World.Entities.Remove(_ring4);
            GameService.Graphics.World.Entities.Remove(_ring5);
        }

        private Color getColor(Colors color)
        {
            switch (color)
            {
                default:
                case Colors.White:
                    return Color.White;
                case Colors.Black:
                    return Color.Black;
                case Colors.Red:
                    return Color.Red;
                case Colors.Green:
                    return Color.Green;
                case Colors.Blue:
                    return Color.Blue;
                case Colors.Magenta:
                    return Color.Magenta;
                case Colors.Cyan:
                    return Color.Cyan;
                case Colors.Yellow:
                    return Color.Yellow;
            }
        }

    }

}
