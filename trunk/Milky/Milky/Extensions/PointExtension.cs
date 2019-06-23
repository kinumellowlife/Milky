using System.Drawing;

namespace Milky.Extensions
{
	public static class PointExtension
	{
		static public Point Plus(this Point pos, int add)
		{
			Point result = new Point(pos.X, pos.Y);
			result.X += add;
			result.Y += add;
			return result;
		}

		static public Point Plus(this Point pos, int addX, int addY)
		{
			Point result = new Point(pos.X, pos.Y);
			result.X += addX;
			result.Y += addY;
			return result;
		}

		static public PointF Plus(this PointF pos, int add)
		{
			PointF result = new PointF(pos.X, pos.Y);
			result.X += add;
			result.Y += add;
			return result;
		}

		static public PointF Plus(this PointF pos, float addX, float addY)
		{
			PointF result = new PointF(pos.X, pos.Y);
			result.X += addX;
			result.Y += addY;
			return result;
		}

		static public bool IsInner(this Rectangle rect, int x, int y)
		{
			if ((rect.X <= x) && (x < (rect.X + rect.Width)) && (rect.Y <= y) && (y < (rect.Y + rect.Height)))
				return true;
			else
				return false;
		}

		static public bool IsInner(this Rectangle rect, Point point)
		{
			return rect.IsInner(point.X, point.Y);
		}

		static public bool IsInner(this RectangleF rect, float x, float y)
		{
			if ((rect.X <= x) && (x <= rect.X) && (rect.Y <= y) && (y <= rect.Y))
				return true;
			else
				return false;
		}

		static public bool IsInner(this RectangleF rect, int x, int y)
		{
			if ((rect.X <= x) && (x <= rect.X) && (rect.Y <= y) && (y <= rect.Y))
				return true;
			else
				return false;
		}

		static public bool IsInner(this RectangleF rect, Point point)
		{
			return rect.IsInner(point.X, point.Y);
		}

		static public bool IsInner(this RectangleF rect, PointF point)
		{
			return rect.IsInner(point.X, point.Y);
		}
	}
}