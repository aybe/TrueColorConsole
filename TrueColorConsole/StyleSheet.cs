using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TrueColorConsole;

namespace TrueColorConsole.Styled {
	public class StyleSheet {
		public StyleSheet() { }
		public StyleSheet(Color default_color) {
			this.default_color = default_color;
		}
		public Color default_color = Color.Empty;
		public List<iRule> rules = new List<iRule>();
		public void AddStyle(iRule rule) => rules.Add(rule);
		public IEnumerable<SegmentItem> ApplyRules(String str) {
			var items = new List<SegmentItem>();
			var def = new SegmentItem {str=str };
			if (default_color != Color.Empty && default_color != Color.Transparent) {
				def.before = new Action[] { () => VTConsole.SetColorForeground(default_color) };
				def.after = new Action[] { () => VTConsole.PopForegroundColor() };
			}
			items.Add(def);
			foreach (var rule in rules)
				rule.ApplyRuleToSegments(items);
			return items;
		}
	}
	public class SegmentItem {
		public string str;
		public IEnumerable<Action> before;
		public IEnumerable<Action> after;
	}

}
