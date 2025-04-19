using HomeManager.Common;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using System.Xml.Linq;

namespace HomeManager.Helpers
{
	public class clsRTBLayout : clsCommonModelPropertiesBase
	{
		/*deze method gaat de layout ophalen van geselecteerder text
		 * de behavior gaat deze functie gebruiken
		 */
		public void UpdateLayoutFromSelection(TextRange range)
		{
			MyFontWeight = GetFontWeight(range);
			MyTextDecorations = GetMyDecorations(range);
			MyFontStyle = GetFontStyles(range);
			GetColorsFromSelectionChanged(range);
			MyFondSize = ReturnFontSizeFromSelectionChanged(range);
			SelectedFond = GetFontFamilyFromSelection(range);
			MyTextAlignment = GetTextAlignmentFromSelection(range);
			//MyTypographyVariant = GetTypographyFromSelection(range);
			//UpdateTypographyFromSelection(range);
		}

		//deze bool kan ik gebruiken om mijn selectionchanged af te zetten
		public bool SelectionChangedIsEnabled = true;

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
				range.ApplyPropertyValue(Inline.TextDecorationsProperty, MyTextDecorations);
			}
		}

		public void ToggleTrikeTrough(TextRange range)
		{
			if (IsStrikeTrough)
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
				MyTextDecorations.Add(decoration);

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

		#region TextColor
		private clsDagboekCustomColor _selectedTextColor = new clsDagboekCustomColor()
		{
			ColorName = "Black",
			MyColor = Colors.Black
		};

		public clsDagboekCustomColor SelectedTextColor
		{
			get { return _selectedTextColor; }
			set
			{
				_selectedTextColor = value;
				OnPropertyChanged();
			}
		}

		private clsDagboekCustomColor _selectedTextBackgroundColor = new clsDagboekCustomColor()
		{
			ColorName = "White",
			MyColor = Colors.White
		};

		public clsDagboekCustomColor SelectedTextBackgroundColor
		{
			get { return _selectedTextBackgroundColor; }
			set
			{
				_selectedTextBackgroundColor = value;
				OnPropertyChanged();
			}
		}


		private ObservableCollection<clsDagboekCustomColor> _colorsCollection;

		public ObservableCollection<clsDagboekCustomColor> ColorsCollection
		{
			get
			{
				if (_colorsCollection.IsNullOrEmpty() || !_colorsCollection.Any())
				{
					_colorsCollection = new ObservableCollection<clsDagboekCustomColor>();
					PopulateColors();
					OnPropertyChange(nameof(ColorsCollection));
				}
				return _colorsCollection;
			}
		}

		private void PopulateColors()
		{
			if (_colorsCollection.Any()) return; // Prevent duplicate additions

			// Get all properties of the Colors class
			var colorProperties = typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static);

			// Loop through each property and add to the collection
			foreach (var prop in colorProperties)
			{
				var color = (System.Windows.Media.Color)prop.GetValue(null);
				_colorsCollection.Add(new clsDagboekCustomColor
				{
					ColorName = prop.Name,
					MyColor = color
				});
			}
		}

		public void GetColorsFromSelectionChanged(TextRange range)
		{
			// Get the Foreground (Brush) of the selection
			var textColorProperty = range.GetPropertyValue(TextElement.ForegroundProperty) as SolidColorBrush;

			if (textColorProperty != null) // Ensure it's a SolidColorBrush
			{
				// Extract the Color from the Brush
				var textColor = textColorProperty.Color;

				// Find the matching color in the ColorsCollection
				SelectedTextColor = ColorsCollection.SingleOrDefault(color => color.MyColor.Equals(textColor));

				// Optionally handle the case where no match is found
				if (SelectedTextColor == null)
				{
					// Default to Black or handle as needed
					SelectedTextColor = ColorsCollection.FirstOrDefault(c => c.ColorName == "Black");
				}
			}
			else
			{
				// If no brush, default to Black or any fallback color
				SelectedTextColor = ColorsCollection.FirstOrDefault(c => c.ColorName == "Black");
			}

			// Get the Background (Brush) of the selection
			var textBackgroundColorProperty = range.GetPropertyValue(TextElement.BackgroundProperty) as SolidColorBrush;


			// Handle the Background (background color)
			if (textBackgroundColorProperty != null) // Ensure it's a SolidColorBrush
			{
				var textBackgroundColor = textBackgroundColorProperty.Color;

				// Find the matching color in the ColorsCollection for the background
				SelectedTextBackgroundColor = ColorsCollection.SingleOrDefault(color => color.MyColor.Equals(textBackgroundColor));

				// Optionally handle the case where no match is found
				if (SelectedTextBackgroundColor == null)
				{
					// Default to White or handle as needed
					SelectedTextBackgroundColor = ColorsCollection.FirstOrDefault(c => c.ColorName == "White");
				}
			}
			else
			{
				// If no brush, default to White or any fallback color for the background
				SelectedTextBackgroundColor = ColorsCollection.FirstOrDefault(c => c.ColorName == "White");
			}
		}

		public void ApplyForegroundColorToTextRange(TextRange range)
		{
			SolidColorBrush brush = new SolidColorBrush(SelectedTextColor.MyColor);
			range.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
		}

		public void ApplyTextBackgroundColorToTextRange(TextRange range)
		{
			SolidColorBrush brush = new SolidColorBrush(SelectedTextBackgroundColor.MyColor);
			range.ApplyPropertyValue(TextElement.BackgroundProperty, brush);
		}
		#endregion

		#region TextFont and TextSize
		private FontFamily _selectedFond;

		public FontFamily SelectedFond
		{
			get
			{
				if (_selectedFond == null && MyFonds.Any())
				{
					_selectedFond = MyFonds.FirstOrDefault();
				}
				return _selectedFond;
			}
			set
			{
				_selectedFond = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<FontFamily> _myFonds;

		public ObservableCollection<FontFamily> MyFonds
		{
			get
			{
				if (_myFonds.IsNullOrEmpty() || !_myFonds.Any())
				{
					_myFonds = new ObservableCollection<FontFamily>(Fonts.SystemFontFamilies);
					OnPropertyChange(nameof(MyFonds));
				}
				return _myFonds;
			}
		}

		private double _myFondSize = 12;

		public double MyFondSize
		{
			get
			{
				return _myFondSize;
			}

			set
			{
				_myFondSize = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(IsBiggestFondSize));
				OnPropertyChanged(nameof(IsSmallestFondSize));
			}
		}

		public bool IsSmallestFondSize
		{
			get 
			{
				if (_myFondSize == FondSizes[0])
				{
					return true;
				}
				return false;
			}
		}

		public bool IsBiggestFondSize
		{
			get
			{
				if (_myFondSize == FondSizes[FondSizes.Length - 1])
				{
					return true;
				}
				return false;
			}
		}
			
		public double[] FondSizes { get; } = new double[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27 };

		public double ReturnFontSizeFromSelectionChanged(TextRange range)
		{
			object selection = range.GetPropertyValue(TextElement.FontSizeProperty);
			if (selection is double size)
			{
				return size;
			}
			return _myFondSize;
		}

		public void SetFondSize(TextRange range)
		{
			range.ApplyPropertyValue(TextElement.FontSizeProperty, MyFondSize);
		}

		public FontFamily GetFontFamilyFromSelection(TextRange range)
		{
			object selection = range.GetPropertyValue(TextElement.FontFamilyProperty);
			if (selection is FontFamily family)
			{
				return family;
			}
			return SelectedFond;
		}

		public void SetFondFamily(TextRange range)
		{
			range.ApplyPropertyValue(TextElement.FontFamilyProperty, SelectedFond);
		}

		#endregion

		#region SubScript & SupperScript
		//private BaselineAlignment _currentBaseline = BaselineAlignment.Baseline;

		//public void UpdateTypographyFromSelection(TextRange range)
		//{
		//    object safetyObject = range.GetPropertyValue(Inline.BaselineAlignmentProperty);
		//    if (safetyObject is BaselineAlignment alignment)
		//    {
		//        _currentBaseline = alignment;
		//    }
		//    else
		//    {
		//        _currentBaseline = BaselineAlignment.Baseline; // Default to normal
		//    }

		//    // Notify UI that the buttons need to be updated
		//    OnPropertyChanged(nameof(IsSubscript));
		//    OnPropertyChanged(nameof(IsSuperscript));
		//}

		//public bool IsSubscript
		//{
		//    get
		//    {
		//        return _currentBaseline == BaselineAlignment.Subscript;
		//    }
		//}

		//public bool IsSuperscript
		//{
		//    get
		//    {
		//        return _currentBaseline == BaselineAlignment.Superscript;
		//    }
		//}

  //      public FontVariants GetTypographyFromSelection (TextRange range)
		//{
		//	object safetyObject = range.GetPropertyValue(Typography.VariantsProperty);
		//	if (safetyObject is FontVariants variant )
		//	{
		//		return variant;
		//	}
		//	return MyTypographyVariant;
		//}

		//public void SetSelectionToSubscript(TextRange range)
		//{
		//	//als subscript al aanstaat zetten we dit uit
		//	if (MyTypographyVariant == FontVariants.Subscript)
		//	{
  //              range.ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Normal);
  //          }
		//	else
		//	{
  //              range.ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Subscript);
  //          }
  //          MyTypographyVariant = GetTypographyFromSelection(range);
  //      }

		//public void SetSelectionToSupperScript(TextRange range)
		//{
		//	//als supperscript al aanstaat zetten we dit uit
		//	if (MyTypographyVariant == FontVariants.Superscript)
		//	{
		//		range.ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Normal);
		//	}
		//	else
		//	{
		//		range.ApplyPropertyValue(Typography.VariantsProperty, FontVariants.Superscript);
		//	}
		//	MyTypographyVariant = GetTypographyFromSelection(range);
		//}

		public void SetSelectionToSubscript(TextRange range)
		{
			if (range.IsEmpty) return; // Don't apply formatting to an empty selection

			object currentBaseline = range.GetPropertyValue(Inline.BaselineAlignmentProperty);

			if (currentBaseline is BaselineAlignment alignment && alignment == BaselineAlignment.Subscript)
			{
				// Reset to normal text
				range.ApplyPropertyValue(Inline.BaselineAlignmentProperty, BaselineAlignment.Baseline);
				range.ApplyPropertyValue(TextElement.FontSizeProperty, MyFondSize); // Reset font size
			}
			else
			{
				// Apply subscript formatting
				range.ApplyPropertyValue(Inline.BaselineAlignmentProperty, BaselineAlignment.Subscript);
				range.ApplyPropertyValue(TextElement.FontSizeProperty, MyFondSize - 2); // Adjust font size for subscript effect
			}
		}

		public void SetSelectionToSuperscript(TextRange range)
		{
			if (range.IsEmpty) return; // Don't apply formatting to an empty selection

			object currentBaseline = range.GetPropertyValue(Inline.BaselineAlignmentProperty);

			if (currentBaseline is BaselineAlignment alignment && alignment == BaselineAlignment.Superscript)
			{
				// Reset to normal text (Baseline)
				range.ApplyPropertyValue(Inline.BaselineAlignmentProperty, BaselineAlignment.Baseline);
				range.ApplyPropertyValue(TextElement.FontSizeProperty, MyFondSize); // Reset font size
			}
			else
			{
				// Apply superscript formatting
				range.ApplyPropertyValue(Inline.BaselineAlignmentProperty, BaselineAlignment.Superscript);
				range.ApplyPropertyValue(TextElement.FontSizeProperty, MyFondSize - 2); // Adjust font size for superscript effect
			}
		}

		#endregion

		#region AlineaOpmaak
		private TextAlignment? _myTextAlignment = TextAlignment.Left;

		public TextAlignment? MyTextAlignment
		{
			get { return _myTextAlignment; }
			set 
			{ 
				_myTextAlignment = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(IsLeftTextAlignment));
				OnPropertyChanged(nameof(IsRightTextAlignment));
				OnPropertyChanged(nameof(IsCenterTextAlignment));
				OnPropertyChanged(nameof(IsJustifyTextAlignment));
			}
		}
		
		public bool IsLeftTextAlignment
		{
			get
			{
				if (MyTextAlignment == TextAlignment.Left)
				{
					return true;
				}
				return false;
			}
		}

		public bool IsRightTextAlignment
		{
			get
			{
				if (MyTextAlignment == TextAlignment.Right)
				{
					return true;
				}
				return false;
			}
		}

		public bool IsCenterTextAlignment
		{
			get
			{
				if (MyTextAlignment == TextAlignment.Center)
				{
					return true;
				}
				return false;
			}
		}

		public bool IsJustifyTextAlignment
		{
			get
			{
				if (MyTextAlignment == TextAlignment.Justify)
				{
					return true;
				}
				return false;
			}
		}

		public TextAlignment? GetTextAlignmentFromSelection(TextRange range)
		{
			if (range.IsEmpty)
			{
				return null;
			}
			else
			{
				var myParagraph = range.Start.Paragraph;
				if (myParagraph == null)
				{
					return TextAlignment.Left;
				}
				return myParagraph.TextAlignment;
			}
		}

		public void SetTextAlignmentToSelection(TextRange range, TextAlignment alignment)
		{
			var paragraph = range.Start.Paragraph;
			if (paragraph == null) return;

			switch (alignment)
			{
				case TextAlignment.Right:
					MyTextAlignment = TextAlignment.Right;
					paragraph.TextAlignment = (TextAlignment)MyTextAlignment;
					break;

				case TextAlignment.Center:
					MyTextAlignment = TextAlignment.Center;
					paragraph.TextAlignment = (TextAlignment)MyTextAlignment;
					break;

				case TextAlignment.Justify:
					MyTextAlignment = TextAlignment.Justify;
					paragraph.TextAlignment = (TextAlignment)MyTextAlignment;
					paragraph.Inlines.Add(new LineBreak()); //justify werkt aleen met meerdere regels.
					break;

				default:
					MyTextAlignment = TextAlignment.Left;
					paragraph.TextAlignment = (TextAlignment)MyTextAlignment;
					break;

			}
		}


		private double _myTextIndentation = 0;

		public double MyTextIndentation
		{
			get { return _myTextIndentation; }
			set 
			{ 
				_myTextIndentation = value;
				OnPropertyChanged();
			}
		}

		public void IncreaseTextIndentation(IEnumerable<Paragraph> paragraphs)
		{
			foreach (Paragraph paragraph in paragraphs)
			{
				paragraph.TextIndent += 20;
			}

			MyTextIndentation += 20;
		}

		public void DecreaseTextIndentation(IEnumerable<Paragraph> paragraphs)
		{
			if (MyTextIndentation == 0) return;

			foreach (Paragraph paragraph in paragraphs)
			{
				if (paragraph.TextIndent >= 20)
				{
					paragraph.TextIndent -= 20;
				}
			}

			MyTextIndentation -= 20;
		}

		#endregion

		}
}
