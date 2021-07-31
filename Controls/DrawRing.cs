using Blish_HUD;
using Blish_HUD.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Manlaan.DistanceRings.Control
{
    public class DrawRing : IEntity
    {
        public Texture2D RingTexture;
        public float RingOpacity;
        public bool RingVisible;
        public Color RingColor;
        private VertexPositionColorTexture[] vertex;

        private VertexBuffer _geometryBuffer;

        public Vector3 Size { get; set; }

        public float VerticalOffset { get; set; }

        public float DrawOrder => 0;

        private static BasicEffect _renderEffect;

        public DrawRing()
        {
            _renderEffect = _renderEffect ?? new BasicEffect(GameService.Graphics.GraphicsDevice);

            this.Size = Vector3.Zero;
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



        public void Render(GraphicsDevice graphicsDevice, IWorld world, ICamera camera)
        {
            if (this.RingVisible && _geometryBuffer != null)
            {
                float x = GameService.Gw2Mumble.PlayerCharacter.Position.X;
                float y = GameService.Gw2Mumble.PlayerCharacter.Position.Y;
                float z = GameService.Gw2Mumble.PlayerCharacter.Position.Z + VerticalOffset;

                float facing = (float)(Math.Atan2(GameService.Gw2Mumble.PlayerCamera.Forward.X, GameService.Gw2Mumble.PlayerCamera.Forward.Y) * 180 / Math.PI);
                Matrix worldMatrix = Matrix.CreateTranslation(x, y, z);
                worldMatrix.M11 = (float)(Math.Cos(MathHelper.ToRadians(facing)));
                worldMatrix.M12 = (float)(-Math.Sin(MathHelper.ToRadians(facing)));
                worldMatrix.M21 = (float)(Math.Sin(MathHelper.ToRadians(facing)));
                worldMatrix.M22 = (float)(Math.Cos(MathHelper.ToRadians(facing)));
                
                _renderEffect.View = GameService.Gw2Mumble.PlayerCamera.View;
                _renderEffect.Projection = GameService.Gw2Mumble.PlayerCamera.Projection;
                _renderEffect.World = worldMatrix;
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

        public void Update(GameTime gameTime) { /* NOOP */ }
    }
}