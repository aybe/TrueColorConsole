using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace TrueColorConsole.Styled {
	public interface iRule {
		void ApplyRuleToSegments(List<SegmentItem> items);
	}
	public class FormatRule : RawRule {
		public FormatRule(Regex regex, IEnumerable<VTFormat> before, IEnumerable<VTFormat> after, Func<string, string> match_replace = null)
			: base(regex, new Action[] { () => VTConsole.SetFormat(before.ToArray()) }, new Action[] { () => VTConsole.SetFormat(after.ToArray()) }, match_replace) {
		}
	}
	public class RawRule : iRule {
		public string only_use_capture_group_name;//if set only operate on this capture group rather than entire match
		public RawRule(Regex regex, IEnumerable<Action> before, IEnumerable<Action> after, Func<string, string> match_replace = null) {
			this.regex = regex;
			this.match_replace = match_replace;
			this.before_cmds = before;
			this.after_cmds = after;
		}
		public void ApplyRuleToSegments(List<SegmentItem> items) {
			foreach (var item in items.ToArray()) {
				var cur_item = item;
				var matches = regex.Matches(cur_item.str);
				foreach (var entire_match in matches.Cast<Match>().OrderByDescending(a => a.Index)) {//need to work back to front, do we?
					if (!entire_match.Success)
						continue;
					var match = string.IsNullOrWhiteSpace(only_use_capture_group_name) ? entire_match.Groups[only_use_capture_group_name] : entire_match;
					var pos = match.Index;
					//So we want to allow other items affecting the segment( ie uderline bg color etc) to conintue through that we don't set
					var before_segment = new SegmentItem { str = cur_item.str.Substring(0, pos), before = cur_item.before };
					var after_segment = new SegmentItem { str = cur_item.str.Substring(pos + match.Length), after = cur_item.after };
					//even if there is nothing before or after we want to add them for styling
					items.Insert(items.IndexOf(cur_item) + 1, after_segment);
					items.Insert(items.IndexOf(cur_item), before_segment);

					cur_item.str = cur_item.str.Substring(pos, match.Length);

					if (match_replace != null)
						cur_item.str = match_replace(cur_item.str);

					cur_item.before = before_cmds;
					cur_item.after = after_cmds;
					cur_item = before_segment;//we work back to front
				}
			}
		}
		protected IEnumerable<Action> before_cmds;
		protected IEnumerable<Action> after_cmds;
		protected Func<string, string> match_replace;
		protected Regex regex;
	}
	public class StyleRule : RawRule {
		public StyleRule(String regex, Color foreground_color, Func<string, string> match_replace = null, Color? background_color = null) : this(new Regex(regex), foreground_color, match_replace, background_color) {

		}
		protected static IEnumerable<Action> GetCommands(Color foreground_color, Color? background_color, bool need_after) {
			var cmds = new List<Action>();
			if (foreground_color != Color.Empty && foreground_color != Color.Transparent) {
				if (!need_after)
					cmds.Add(() => VTConsole.SetColorForeground(foreground_color));
				else
					cmds.Add(() => VTConsole.PopForegroundColor());
			}
			if (background_color != null && background_color != Color.Empty && background_color != Color.Transparent) {
				if (!need_after)
					cmds.Add(() => VTConsole.SetColorBackground(background_color.Value));
				else
					cmds.Add(() => VTConsole.PopBackgroundColor());
			}

			return cmds;
		}
		public StyleRule(Regex regex, Color foreground_color, Func<string, string> match_replace = null, Color? background_color = null) :
			base(regex, GetCommands(foreground_color, background_color, false), GetCommands(foreground_color, background_color, true), match_replace) {

		}


	}

}
