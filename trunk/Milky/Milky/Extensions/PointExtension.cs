using System.Drawing;

namespace Milky.Extensions
{
	public static class PointExtension
	{
		/// <summary>
		/// XY方向に等しく加算
		/// </summary>
		/// <param name="pos">基準点</param>
		/// <param name="add">加算量</param>
		/// <returns>新しいPoint</returns>
		static public Point Plus(this Point pos, int add)
		{
			Point result = new Point(pos.X, pos.Y);
			result.X += add;
			result.Y += add;
			return result;
		}

		/// <summary>
		/// XY方向それぞれに加算
		/// </summary>
		/// <param name="pos">基準点</param>
		/// <param name="addX">X加算量</param>
		/// <param name="addY">Y加算量</param>
		/// <returns>新しいPoint</returns>
		static public Point Plus(this Point pos, int addX, int addY)
		{
			Point result = new Point(pos.X, pos.Y);
			result.X += addX;
			result.Y += addY;
			return result;
		}

		/// <summary>
		/// XY方向に等しく加算
		/// </summary>
		/// <param name="pos">基準点</param>
		/// <param name="add">加算量</param>
		/// <returns>新しいPoint</returns>
		static public PointF Plus(this PointF pos, int add)
		{
			PointF result = new PointF(pos.X, pos.Y);
			result.X += add;
			result.Y += add;
			return result;
		}

		/// <summary>
		/// XY方向それぞれに加算
		/// </summary>
		/// <param name="pos">基準点</param>
		/// <param name="addX">X加算量</param>
		/// <param name="addY">Y加算量</param>
		/// <returns>新しいPoint</returns>
		static public PointF Plus(this PointF pos, float addX, float addY)
		{
			PointF result = new PointF(pos.X, pos.Y);
			result.X += addX;
			result.Y += addY;
			return result;
		}

		/// <summary>
		/// 指定座標が含まれるかどうか
		/// </summary>
		/// <param name="rect">範囲</param>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <returns>含む場合はTrue</returns>
		static public bool IsInner(this Rectangle rect, int x, int y)
		{
			if ((rect.X <= x) && (x < (rect.X + rect.Width)) && (rect.Y <= y) && (y < (rect.Y + rect.Height)))
				return true;
			else
				return false;
		}

		/// <summary>
		/// 指定座標が含まれるかどうか
		/// </summary>
		/// <param name="rect">範囲</param>
		/// <param name="point">座標</param>
		/// <returns>含む場合はTrue</returns>
		static public bool IsInner(this Rectangle rect, Point point)
		{
			return rect.IsInner(point.X, point.Y);
		}

		/// <summary>
		/// 指定座標が含まれるかどうか
		/// </summary>
		/// <param name="rect">範囲</param>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <returns>含む場合はTrue</returns>
		static public bool IsInner(this RectangleF rect, float x, float y)
		{
			if ((rect.X <= x) && (x <= rect.X) && (rect.Y <= y) && (y <= rect.Y))
				return true;
			else
				return false;
		}

		/// <summary>
		/// 指定座標が含まれるかどうか
		/// </summary>
		/// <param name="rect">範囲</param>
		/// <param name="x">X座標</param>
		/// <param name="y">Y座標</param>
		/// <returns>含む場合はTrue</returns>
		static public bool IsInner(this RectangleF rect, int x, int y)
		{
			if ((rect.X <= x) && (x <= rect.X) && (rect.Y <= y) && (y <= rect.Y))
				return true;
			else
				return false;
		}

		/// <summary>
		/// 指定座標が含まれるかどうか
		/// </summary>
		/// <param name="rect">範囲</param>
		/// <param name="point">座標</param>
		/// <returns>含む場合はTrue</returns>
		static public bool IsInner(this RectangleF rect, Point point)
		{
			return rect.IsInner(point.X, point.Y);
		}

		/// <summary>
		/// 指定座標が含まれるかどうか
		/// </summary>
		/// <param name="rect">範囲</param>
		/// <param name="point">座標</param>
		/// <returns>含む場合はTrue</returns>
		static public bool IsInner(this RectangleF rect, PointF point)
		{
			return rect.IsInner(point.X, point.Y);
		}
	}
}