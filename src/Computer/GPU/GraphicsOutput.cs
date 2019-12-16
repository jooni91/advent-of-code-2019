using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace MyAoC2019.Computer.GPU
{
    public static class GraphicsOutput
    {
        /// <summary>
        /// Draw a bitmap of the points specified in <paramref name="grid2D"/>.
        /// </summary>
        /// <param name="grid2D">The pixel map of a 2D space, which contains inforamtion about the color of a pixel.</param>
        /// <param name="size">The size of the bitmap.</param>
        /// <param name="scale">Scale the bitmap size by this factor.</param>
        /// <param name="backgroundColor">The background color of the image. The default is <see cref="Color.White"/>.</param>
        /// <param name="padding">The padding to apply to the bitmap.</param>
        /// <param name="borderColor">If a padding is specified you can define the color for the padding. The default is <see cref="Color.Transparent"/>.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="size"/> width or height is zero or less.</exception>
        public static Bitmap DrawBitmap(Dictionary<Vector2, Color> grid2D, Size size, float scale = 1.0f, Color? backgroundColor = null, Size? padding = null, Color? borderColor = null)
        {
            if (size.Width <= 0 || size.Height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "The size can not contain a zero or negative number.");
            }

            var width = (int)(size.Width * scale);
            var height = (int)(size.Height * scale);
            var _padding = padding ?? new Size(0, 0);

            var bitmap = new Bitmap(width + (_padding.Width * 2), height + (_padding.Height * 2));

            // Draw the border
            if (_padding != Size.Empty)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    for (var y = 0; y < bitmap.Height; y++)
                    {
                        if (y < _padding.Height || y > bitmap.Height - 1 - _padding.Height || x < _padding.Width || x > bitmap.Width - 1 - _padding.Width)
                        {
                            bitmap.SetPixel(x, y, borderColor ?? Color.Transparent);
                        }
                    }
                }
            }

            // Draw the image
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var color = grid2D.ContainsKey(new Vector2((int)(x / scale), (int)(y / scale)))
                        ? grid2D[new Vector2((int)(x / scale), (int)(y / scale))]
                        : backgroundColor ?? Color.White;

                    bitmap.SetPixel(x + _padding.Width, y + _padding.Height, color);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Get the dimensions of a dictionary containing points in a 2 dimensional space as the key values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grid"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the <paramref name="grid"/> does contain less then 2 items. You need at least 2 points in a space.
        /// </exception>
        public static Size GetDimensions<T>(Dictionary<Vector2, T> grid)
        {
            if (grid.Count < 2)
            {
                throw new InvalidOperationException("The argument does contain less then 2 items.");
            }

            var rect = new Rectangle((int)grid.Keys.Min(vect => vect.X), (int)grid.Keys.Min(vect => vect.Y),
                (int)grid.Keys.Max(vect => vect.X), (int)grid.Keys.Max(vect => vect.Y));

            return new Size(rect.Width - rect.X + 1, rect.Height - rect.Y + 1);
        }
    }
}
