using Godot;
using System;
using System.Threading.Tasks;

public partial class TransitionScene : CanvasLayer
{
	AnimationPlayer animationPlayer;

	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");

	}

	public async Task FadeOut()
	{
		animationPlayer.Play("dissolve_out");

		await ToSignal(animationPlayer, "animation_finished");
		return;

	}

	public async Task FadeIn()
	{

		animationPlayer.Play("dissolve_in");
		await ToSignal(animationPlayer, "animation_finished");
		return;

	}
}
