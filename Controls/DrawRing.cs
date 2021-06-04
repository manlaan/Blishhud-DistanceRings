using Blish_HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Blish_HUD.Entities.Primitives;

namespace DistanceRings
{
    class DrawRing : Cuboid
    {
        public Texture2D RingTexture;
        public Vector3 RingPosition;
        public float RingOpacity;
        public bool RingVisible;
        public Color RingColor;

        public DrawRing()
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.Position = RingPosition;
            this.Visible = RingVisible;
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            if (_geometryBuffer == null) return;

            _renderEffect.View = GameService.Gw2Mumble.PlayerCamera.View;
            _renderEffect.Projection = GameService.Gw2Mumble.PlayerCamera.Projection;
            _renderEffect.World = Matrix.CreateTranslation(_position);
            _renderEffect.Texture = RingTexture;
            _renderEffect.Alpha = RingOpacity;
            _renderEffect.VertexColorEnabled = true;

            VertexPositionColorTexture[] v = new VertexPositionColorTexture[4];
            v[0].Position = new Vector3(-1, 1, 1) * Size;
            v[0].TextureCoordinate = new Vector2(0, 0);
            v[0].Color = RingColor;
            v[1].Position = new Vector3(1, 1, 1) * Size;
            v[1].TextureCoordinate = new Vector2(1, 0);
            v[1].Color = RingColor;
            v[2].Position = new Vector3(-1, -1, 1) * Size;
            v[2].TextureCoordinate = new Vector2(0, 1);
            v[2].Color = RingColor;
            v[3].Position = new Vector3(1, -1, 1) * Size;
            v[3].TextureCoordinate = new Vector2(1, 1);
            v[2].Color = RingColor;

            _geometryBuffer = new VertexBuffer(GameService.Graphics.GraphicsDevice, VertexPositionColorTexture.VertexDeclaration, 4, BufferUsage.WriteOnly);
            _geometryBuffer.SetData(v);

            graphicsDevice.SetVertexBuffer(_geometryBuffer, 0);

            foreach (var pass in _renderEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
            }

            graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);         }
        }
}
