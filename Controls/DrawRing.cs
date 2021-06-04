using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Pathing;
using Blish_HUD.Pathing.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Blish_HUD.Pathing;
using Blish_HUD.Pathing.Content;
using Blish_HUD.Pathing.Format;
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
            //this.Opacity = RingOpacity;
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

            /*
                        _verts[0].Position = new Vector3(-1, 1, -1) * Size;
                        _verts[0].TextureCoordinate = new Vector2(0, 0);
                        _verts[1].Position = new Vector3(1, 1, -1) * Size;
                        _verts[1].TextureCoordinate = new Vector2(1, 0);
                        _verts[2].Position = new Vector3(-1, 1, 1) * Size;
                        _verts[2].TextureCoordinate = new Vector2(0, 1);
                        _verts[3].Position = new Vector3(1, 1, 1) * Size;
                        _verts[3].TextureCoordinate = new Vector2(1, 1);

                        _verts[4].Position = new Vector3(-1, -1, 1) * Size;
                        _verts[4].TextureCoordinate = new Vector2(0, 0);
                        _verts[5].Position = new Vector3(1, -1, 1) * Size;
                        _verts[5].TextureCoordinate = new Vector2(1, 0);
                        _verts[6].Position = new Vector3(-1, -1, -1) * Size;
                        _verts[6].TextureCoordinate = new Vector2(0, 1);
                        _verts[7].Position = new Vector3(1, -1, -1) * Size;
                        _verts[7].TextureCoordinate = new Vector2(1, 1);

                        _verts[8].Position = new Vector3(-1, 1, -1) * Size;
                        _verts[8].TextureCoordinate = new Vector2(0, 0);
                        _verts[9].Position = new Vector3(-1, 1, 1) * Size;
                        _verts[9].TextureCoordinate = new Vector2(1, 0);
                        _verts[10].Position = new Vector3(-1, -1, -1) * Size;
                        _verts[10].TextureCoordinate = new Vector2(0, 1);
                        _verts[11].Position = new Vector3(-1, -1, 1) * Size;
                        _verts[11].TextureCoordinate = new Vector2(1, 1);

                        _verts[12].Position = new Vector3(-1, 1, 1) * Size;
                        _verts[12].TextureCoordinate = new Vector2(0, 0);
                        _verts[13].Position = new Vector3(1, 1, 1) * Size;
                        _verts[13].TextureCoordinate = new Vector2(1, 0);
                        _verts[14].Position = new Vector3(-1, -1, 1) * Size;
                        _verts[14].TextureCoordinate = new Vector2(0, 1);
                        _verts[15].Position = new Vector3(1, -1, 1) * Size;
                        _verts[15].TextureCoordinate = new Vector2(1, 1);

                        _verts[16].Position = new Vector3(1, 1, 1) * Size;
                        _verts[16].TextureCoordinate = new Vector2(0, 0);
                        _verts[17].Position = new Vector3(1, 1, -1) * Size;
                        _verts[17].TextureCoordinate = new Vector2(1, 0);
                        _verts[18].Position = new Vector3(1, -1, 1) * Size;
                        _verts[18].TextureCoordinate = new Vector2(0, 1);
                        _verts[19].Position = new Vector3(1, -1, -1) * Size;
                        _verts[19].TextureCoordinate = new Vector2(1, 1);

                        _verts[20].Position = new Vector3(1, 1, -1) * Size;
                        _verts[20].TextureCoordinate = new Vector2(0, 0);
                        _verts[21].Position = new Vector3(-1, 1, -1) * Size;
                        _verts[21].TextureCoordinate = new Vector2(1, 0);
                        _verts[22].Position = new Vector3(1, -1, -1) * Size;
                        _verts[22].TextureCoordinate = new Vector2(0, 1);
                        _verts[23].Position = new Vector3(-1, -1, -1) * Size;
                        _verts[23].TextureCoordinate = new Vector2(1, 1);

            _geometryBuffer = new VertexBuffer(GameService.Graphics.GraphicsDevice, VertexPositionTexture.VertexDeclaration, 24, BufferUsage.WriteOnly);
            _geometryBuffer.SetData(_verts);

             */


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

            //graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 23);
            //graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 12, 2);
            graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);         }
        }
}
