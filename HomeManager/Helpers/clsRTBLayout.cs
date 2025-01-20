using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
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
		private TextDecorationCollection _myTextDecorations = new TextDecorationCollection();

		public TextDecorationCollection MyTextDecorations
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

		public TextDecorationCollection GetMyDecorations(TextRange range)
		{
			var mijnDecorations = range.GetPropertyValue(Inline.TextDecorationsProperty);

			return mijnDecorations as TextDecorationCollection ?? new TextDecorationCollection();

		}

		public void ToggleUnderline(TextRange range)
		{
			if (IsUnderline)
			{
				//remove underline decoration
				var collection = MyTextDecorations.Where(decoration => decoration.Location != TextDecorationLocation.Underline);
                MyTextDecorations = new TextDecorationCollection(collection);

				//apply the changes to the textrange
				range.ApplyPropertyValue(Inline.TextDecorationsProperty, MyTextDecorations);
			}
			else
			{
				//add the decoration
				TextDecoration decoration = new TextDecoration();
				decoration.Location = TextDecorationLocation.Underline;
				MyTextDecorations.Add(decoration);

				//aply the underline
				range.ApplyPropertyValue(Inline.TextDecorationsProperty,MyTextDecorations);
			}
		}

        public void ToggleTrikeTrough(TextRange range)
        {
            if (MyTextDecorations.Any(decoration => decoration.Location == TextDecorationLocation.Strikethrough))
            {
				//remove striketrough decoration
				var collection = MyTextDecorations.Where(decoration => decoration.Location != TextDecorationLocation.Strikethrough);
				MyTextDecorations = new TextDecorationCollection(collection);

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
		private FontStyle _myFontStyle = FontStyles.Normal;

		public FontStyle MyFontStyle
		{
			get { return _myFontStyle; }
			set 
			{ 
				_myFontStyle = value; 
				OnPropertyChanged();
				OnPropertyChanged(nameof(IsItalic));
			}
		}

		public bool IsItalic
		{
			get
			{
				if (_myFontStyle == FontStyles.Italic)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public FontStyle GetFontStyles(TextRange range)
		{
			var fontStyle = range.GetPropertyValue(TextElement.FontStyleProperty);


            if (fontStyle != DependencyProperty.UnsetValue && fontStyle is FontStyle)
			{
      
				return (FontStyle)fontStyle;
            }
			
			return FontStyles.Normal;
		}

		public void ToggleItalic(TextRange range)
		{
			FontStyle font = GetFontStyles(range);
			if (font == FontStyles.Normal)
			{
				MyFontStyle = FontStyles.Italic;
            }
			else
			{
				MyFontStyle = FontStyles.Normal;
			}
            range.ApplyPropertyValue(TextElement.FontStyleProperty, MyFontStyle);
        }

		#endregion

		/*deze method gaat de layout ophalen van geselecteerder text
		 * de behavior gaat deze functie gebruiken
		 */
		public void UpdateLayoutFromSelection(TextRange range)
		{
			MyFontWeight = GetFontWeight(range);
			
			var currentDecorations = GetMyDecorations(range);
			if (!currentDecorations.SequenceEqual(MyTextDecorations))
			{
				MyTextDecorations = currentDecorations;
			}
			
			MyFontStyle = GetFontStyles(range);
			
		}
	}
}
