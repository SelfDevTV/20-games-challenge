shader_type canvas_item;

uniform float progress: hint_range(0.0, 1.0);
uniform sampler2D noise_texture;

void fragment() {
	// Called for every pixel the material is visible on.
	float noise_value = texture(noise_texture, UV).r;
	float treshold = 1.0 - progress;

	if(noise_value < treshold) {
		discard;
	} else {
		COLOR = texture(TEXTURE, UV);
	}

}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
