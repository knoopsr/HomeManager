using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

namespace HomeManager.Helpers
{
    public class clsRTBLayout : clsCommonModelPropertiesBase
    {
        #region Bold
        private FontWeight _myFontWeight = FontWeights.Regular;

		public FontWeight MyFontWeight
		{
			get { return _myFontWeight; }
			set 
			{
				_myFontWeight = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(IsBold));
			}
		}

		public bool IsBold 
		{ 
			get
			{
				if (_myFontWeight == FontWeights.Bold)
				{  
					return true; 
				}
				else
				{
					return false;
				}
			}
		}

		public FontWeight GetFontWeight(TextRange range)
		{
			var propertyValue = range.GetPropertyValue(TextElement.FontWeightProperty);

			if (propertyValue is FontWeight fontWeight)
			{
				return fontWeight;
			}
			else
			{
				//MessageBox.Show(propertyValue.ToString()); bij startup is deze null
				return FontWeights.Regular;
			}
;

        }

		//this is for the command
		public void SetFontWeight(FontWeight fontWeight, TextRange range)
		{
			if (fontWeight == FontWeights.Regular)
			{
				MyFontWeight = FontWeights.Bold;
                range.ApplyPropertyValue(TextElement.FontWeightProperty, MyFontWeight);
            }
			else
			{
                MyFontWeight = FontWeights.Regular;
                range.ApplyPropertyValue(TextElement.FontWeightProperty, MyFontWeight);
            }
		}
		#endregion

		#region Underline & striketrough
		private List<TextDecoration> _myTextDecorations = new List<TextDecoration>();

		public List<TextDecoration> MyTextDecorations
		{
			get { return _myTextDecorations; }
			set 
			{ 
				_myTextDecorations = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(IsUnderline));
				OnPropertyChanged(nameof(IsStrikeTrough));
			}
		}


		public bool IsUnderline
		{
			get
			{
				return MyTextDecorations.Any(decoration => decoration.Location == TextDecorationLocation.Underline);
			}
		}

		public bool IsStrikeTrough
		{
            get
            {
                return MyTextDecorations.Any(decoration => decoration.Location == TextDecorationLocation.Strikethrough);
            }
        }

		public List<TextDecoration> GetMyDecorations(TextRange range)
		{
			var mijnDecorations = range.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection;
			if (mijnDecorations != null && mijnDecorations.Count>0)
			{
				return mijnDecorations.ToList();
			}
			else
			{
				
				return new List<TextDecoration>();
			}
		}

		public void ToggleUnderline(TextRange range)
		{
			if (IsUnderline)
			{
				//remove underline decoration
				MyTextDecorations.RemoveAll(decoration => decoration.Location == TextDecorationLocation.Underline);

				//apply the changes to the textrange
				range.ApplyPropertyValue(Inline.TextDecorationsProperty, new TextDecorationCollection(MyTextDecorations));
			}
			else
			{
				//add the decoration
				TextDecoration decoration = new TextDecoration();
				decoration.Location = TextDecorationLocation.Underline;
				MyTextDecorations.Add(decoration);

				//aply the underline
				range.ApplyPropertyValue(Inline.TextDecorationsProperty,new TextDecorationCollection(MyTextDecorations));
			}
		}

        public void ToggleTrikeTrough(TextRange range)
        {
            if (MyTextDecorations.Any(decoration => decoration.Location == TextDecorationLocation.Strikethrough))
            {
                //remove striketrough decoration
                MyTextDecorations.RemoveAll(decoration => decoration.Location == TextDecorationLocation.Strikethrough);

                //apply the changes to the textrange
                range.ApplyPropertyValue(Inline.TextDecorationsProperty, MyTextDecorations);
            }
            else
            {
                //add the decoration
                TextDecoration decoration = new TextDecoration();
                decoration.Location = TextDecorationLocation.Strikethrough;
                MyTextDecorations.Append(decoration);

                //aply the underline
                range.ApplyPropertyValue(Inline.TextDecorationsProperty, MyTextDecorations);
            }
        }
        #endregion

        #region Italic

        #endregion

        /*deze method gaat de layout ophalen van geselecteerder text
		 * de behavior gaat deze functie gebruiken
		 */
        public void UpdateLayoutFromSelection(TextRange range)
		{
			MyFontWeight = GetFontWeight(range);
			MyTextDecorations = GetMyDecorations(range);
		}
	}
}
