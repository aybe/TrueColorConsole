using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TrueColorConsole.Styled;
namespace TrueColorConsole {
	partial class VTConsole {
		public static void WriteStyled(String value, StyleSheet styleSheet) {
			track_color_history = true;
			var items = styleSheet.ApplyRules(value);
			foreach (var item in items) {
				if (item.before != null)
					foreach (var action in item.before)
						action();
				Console.Write(item.str);
				if (item.after != null)
					foreach (var action in item.after)
						action();
			}
			SetFormat(VTFormat.Default);
			track_color_history = false;
			foreground_color_history.Clear();
			background_color_history.Clear();
		}
		public static void WriteLineStyled(string value, StyleSheet styleSheet) {
			WriteStyled(value, styleSheet);
			Console.WriteLine();
		}
		private static ConcurrentStack<Color> foreground_color_history = new ConcurrentStack<Color>();
		private static ConcurrentStack<Color> background_color_history = new ConcurrentStack<Color>();
		private static bool track_color_history;
		public static void PopForegroundColor() {
			if (foreground_color_history.TryPop(out Color cur)) {
				if (foreground_color_history.TryPop(out Color prev))
					SetColorForeground(prev);//will auto requeue it
				else//set back to default color
					SetFormat(VTFormat.ForegroundDefault);

			}
		}
		public static void PopBackgroundColor() {
			if (background_color_history.TryPop(out Color cur)) {
				if (background_color_history.TryPop(out Color prev))
					SetColorBackground(prev);//will auto requeue it
				else//set back to default color
					SetFormat(VTFormat.BackgroundDefault);

			}
		}

	}
}
