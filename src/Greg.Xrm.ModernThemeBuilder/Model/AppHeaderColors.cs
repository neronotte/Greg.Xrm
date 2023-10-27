using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace Greg.Xrm.ModernThemeBuilder.Model
{
	public class AppHeaderColors : ICloneable
	{
		public Color this[int index]
		{
			get
			{
				switch(index)
				{
					case 0: return Background.ToColor();
					case 1: return Foreground.ToColor();
					case 2: return BackgroundHover.ToColor();
					case 3: return ForegroundHover.ToColor();
					case 4: return BackgroundPressed.ToColor();
					case 5: return ForegroundPressed.ToColor();
					case 6: return BackgroundSelected.ToColor();
					case 7: return ForegroundSelected.ToColor();
					default: throw new ArgumentOutOfRangeException(nameof(index));
				}
			}
			private set 
			{
				switch(index)
				{
					case 0: Background = value.ToHtml(); break;
					case 1: Foreground = value.ToHtml(); break;
					case 2: BackgroundHover = value.ToHtml(); break;
					case 3: ForegroundHover = value.ToHtml(); break;
					case 4: BackgroundPressed = value.ToHtml(); break;
					case 5: ForegroundPressed = value.ToHtml(); break;
					case 6: BackgroundSelected = value.ToHtml(); break;
					case 7: ForegroundSelected = value.ToHtml(); break;
					default: throw new ArgumentOutOfRangeException(nameof(index));
				}
			}
		}

		public AppHeaderColors SetColor(int index, Color color)
		{
			var other = (AppHeaderColors)this.Clone();
			other[index] = color;
			return other;
		}


		[Required]
		[XmlAttribute("background")]
		public string Background { get; set; }
		
		[Required]
		[XmlAttribute("foreground")]
		public string Foreground { get; set; }
		
		[Required]
		[XmlAttribute("backgroundHover")]
		public string BackgroundHover { get; set; }
		
		[Required]
		[XmlAttribute("foregroundHover")]
		public string ForegroundHover { get; set; }
		
		[Required]
		[XmlAttribute("backgroundPressed")]
		public string BackgroundPressed { get; set; }
		
		[Required]
		[XmlAttribute("foregroundPressed")]
		public string ForegroundPressed { get; set; }
		
		[Required]
		[XmlAttribute("backgroundSelected")]
		public string BackgroundSelected { get; set; }

		[Required]
		[XmlAttribute("foregroundSelected")]
		public string ForegroundSelected { get; set; }

		public static AppHeaderColors Default { get; } = new AppHeaderColors
		{
			Background = "#742774",
			Foreground = "#FFFFFF",
			BackgroundHover = "#501B51",
			ForegroundHover = "#FFFFFF",
			BackgroundPressed = "#3F153F",
			ForegroundPressed = "#FFFFFF",
			BackgroundSelected = "#742774",
			ForegroundSelected = "#FFFFFF",
		};


		public override int GetHashCode()
		{
			return Background.GetHashCode()
				^ Foreground.GetHashCode()
				^ BackgroundHover.GetHashCode()
				^ ForegroundHover.GetHashCode()
				^ BackgroundPressed.GetHashCode()
				^ ForegroundPressed.GetHashCode()
				^ BackgroundSelected.GetHashCode()
				^ ForegroundSelected.GetHashCode()
				;
		}


		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!(obj is AppHeaderColors other)) return false;

			for (var i = 0; i < 8; i++)
			{
				if (this[i] != other[i]) return false;
			}
			return true;
		}




		public string Update(string template)
		{
			return template
				.Replace("{Background}", Background)
				.Replace("{Foreground}", Foreground)
				.Replace("{BackgroundHover}", BackgroundHover)
				.Replace("{ForegroundHover}", ForegroundHover)
				.Replace("{BackgroundPressed}", BackgroundPressed)
				.Replace("{ForegroundPressed}", ForegroundPressed)
				.Replace("{BackgroundSelected}", BackgroundSelected)
				.Replace("{ForegroundSelected}", ForegroundSelected)
				;
		}

		public object Clone()
		{
			return new AppHeaderColors
			{
				Background = this.Background,
				Foreground = this.Foreground,
				BackgroundHover = this.BackgroundHover,
				ForegroundHover = this.ForegroundHover,
				BackgroundPressed = this.BackgroundPressed,
				ForegroundPressed = this.ForegroundPressed,
				BackgroundSelected = this.BackgroundSelected,
				ForegroundSelected = this.ForegroundSelected,
			};
		}


		public string ToXmlString()
		{
			var serializer = new XmlSerializer(typeof(AppHeaderColors));

			using (var textWriter = new StringWriter())
			{
				serializer.Serialize(textWriter, this);
				return textWriter.ToString();
			}
		}
	}
}
