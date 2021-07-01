using Blish_HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DistanceRings.Control
{
    public class DrawRing : Blish_HUD.Entities.Primitives.Cuboid
    {
        public Texture2D RingTexture;
        public float RingOpacity;
        public bool RingVisible;
        public Color RingColor;
        private VertexPositionColorTexture[] vertex;

        public DrawRing()
        {
            this.Size = new Vector3(0, 0, 0);
            this.RingOpacity = 1f;
            this.RingColor = Color.White;
            this.RingVisible = false;

            _renderEffect.TextureEnabled = true;
            _renderEffect.VertexColorEnabled = true;

            vertex = new VertexPositionColorTexture[4];
        }

        public void UpdateRings()
        {
            vertex[0].Position = new Vector3(-1, 1, 1) * Size;
            vertex[0].TextureCoordinate = new Vector2(0, 0);
            vertex[0].Color = RingColor;
            vertex[1].Position = new Vector3(1, 1, 1) * Size;
            vertex[1].TextureCoordinate = new Vector2(1, 0);
            vertex[1].Color = RingColor;
            vertex[2].Position = new Vector3(-1, -1, 1) * Size;
            vertex[2].TextureCoordinate = new Vector2(0, 1);
            vertex[2].Color = RingColor;
            vertex[3].Position = new Vector3(1, -1, 1) * Size;
            vertex[3].TextureCoordinate = new Vector2(1, 1);
            vertex[3].Color = RingColor;

            _geometryBuffer = new VertexBuffer(GameService.Graphics.GraphicsDevice, VertexPositionColorTexture.VertexDeclaration, 4, BufferUsage.WriteOnly);
            _geometryBuffer.SetData(vertex);
        }

        public override void HandleRebuild(GraphicsDevice graphicsDevice)
        {
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            if (this.RingVisible)
            {
                if (_geometryBuffer == null) return;

                float x = GameService.Gw2Mumble.PlayerCharacter.Position.X;
                float y = GameService.Gw2Mumble.PlayerCharacter.Position.Y;
                float z = GameService.Gw2Mumble.PlayerCharacter.Position.Z + VerticalOffset;

                float facing = (float)(Math.Atan2(GameService.Gw2Mumble.PlayerCamera.Forward.X, GameService.Gw2Mumble.PlayerCamera.Forward.Y) * 180 / Math.PI);
                Matrix world = Matrix.CreateTranslation(x, y, z);
                world.M11 = (float)(Math.Cos(MathHelper.ToRadians(facing)));
                world.M12 = (float)(-Math.Sin(MathHelper.ToRadians(facing)));
                world.M21 = (float)(Math.Sin(MathHelper.ToRadians(facing)));
                world.M22 = (float)(Math.Cos(MathHelper.ToRadians(facing)));

                _renderEffect.View = GameService.Gw2Mumble.PlayerCamera.View;
                _renderEffect.Projection = GameService.Gw2Mumble.PlayerCamera.Projection;
                _renderEffect.World = world;
                _renderEffect.Texture = RingTexture;
                _renderEffect.Alpha = RingOpacity;

                graphicsDevice.SetVertexBuffer(_geometryBuffer, 0);

                foreach (var pass in _renderEffect.CurrentTechnique.Passes)
                    pass.Apply();

                graphicsDevice.DrawPrimitives(
                    PrimitiveType.TriangleStrip,
                    0,
                    2);
            }
        }
    }
}