shader_type canvas_item;

uniform vec4 letter_color : source_color;
uniform float progress : hint_range(0.0, 1.0);

void fragment() {
	// 0 or 1 based on wether pixel is placed behind progress bar line
	float progress_bool = step(progress, UV.x);
	
	// we decide on final color based on progress
	vec4 col = mix(letter_color, COLOR, progress_bool);
	
	COLOR = col;
}
