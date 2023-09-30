shader_type canvas_item;

uniform vec4 backgroundColor : source_color;
uniform float progress : hint_range(0.0, 1.0);

uniform sampler2D screen_texture : hint_screen_texture, filter_nearest, repeat_disable;

void fragment() {
	// if progress bar is ever missaligned between letters and background use
	// user ruler tool to measure start and end of label box in pixels
	// on x axis and put them here:
	float uv0 = 126.0 / 1920.0;
	float uv1 = 224.0 / 1920.0;
	float a = 1.0 / (uv1 - uv0);
	float b = -a * uv0;
	
	vec4 default_color = texture(TEXTURE, UV);
	
	float screenSize = float(textureSize(screen_texture, 0).x);
	
	float val = a * SCREEN_UV.x + b;
	// 0 or 1 based on wether pixel is placed behind progress bar line
	float progress_bool = step(progress, val);
	
	// we decide on final color based on progress
	vec4 col = mix(backgroundColor, default_color, progress_bool);
	
	// default color is used for it's alfa because letters are rectangles
	vec4 masked_color = vec4(col.rgb, default_color.a);
	
	//COLOR = vec4(val,val, val, 1);
	COLOR = masked_color;
}
