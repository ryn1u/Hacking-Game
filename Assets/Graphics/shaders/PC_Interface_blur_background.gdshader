shader_type canvas_item;

uniform float blur : hint_range(0.0, 10.0) = 0.0;
uniform float darken : hint_range(0.0, 1.0) = 1.0;
uniform sampler2D screen_texture : hint_screen_texture, repeat_disable, filter_linear_mipmap;

void fragment() {
	vec4 color = textureLod(screen_texture, SCREEN_UV, blur) * vec4(darken, darken, darken, 1.0);
	COLOR = color;
}
