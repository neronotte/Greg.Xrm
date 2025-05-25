namespace Greg.Xrm.ModernThemeBuilder.AIEngine
{
	public static class Prompt
	{
		public static string ImageAnalyzer => @"Consider the image provided, it represents a company brand.
You are an expert Power App developer who needs to create a Dataverse Model Driven App Theme for the company CRM. 
You need to define the following colors to be applied in the model driven app header: 

- Background – The background color of the app header. This element must be defined for any changes to take effect.
- Foreground – The text color of the app header. If this isn't specified, the system attempts to calculate an appropriate color that has sufficient contrast to the provided background color.
- BackgroundHover – The background color of buttons on the app header when they're hovered over. If no value is specified, the system calculates a color based on the background color.
- ForegroundHover – The text color of buttons on the app header when they're hovered over. If no value is specified, the system attempts to calculate an appropriate color that has sufficient contrast to the backgroundHover color.
- BackgroundPressed – The background color of buttons on the app header when they're pressed. The defaulting logic is the same as backgroundHover.
- ForegroundPressed – The text color of buttons on the app header when they're pressed. The defaulting logic is the same as foregroundHover.
- BackgroundSelected – The background color of buttons on the app header when they're selected. The defaulting logic is the same as backgroundHover.
- ForegroundSelected – The text color of buttons on the app header when they're selected. The defaulting logic is the same as backgroundHover.

As background color for the app header prefer to use a color similar to the accent tone of the company logo, 
and for the foreground color use a color that has sufficient contrast with the background color.

Produce the output as a simple XML document with the following format:

<AppHeaderColors 
  background=""#12783F""
  foreground=""#FFFFFF"" 
  backgroundHover=""#165A31"" 
  foregroundHover=""#FFFFFF""
  backgroundPressed=""#0F1C12""
  foregroundPressed=""#FFFFFF""
  backgroundSelected=""#153D23"" 
  foregroundSelected=""#FFFFFF""
/>

Don't allucinate. Don't emit anything that is not a valid, well-formed XML document.";
	}
}
