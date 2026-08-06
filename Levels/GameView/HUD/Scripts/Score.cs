using System.Runtime.CompilerServices;
using Godot;
using inflatedpufferfish.Saves;
using InflatedPufferfish.Events;
using TkoUtilities.EventBus;
using TkoUtilities.SavingManager;

public partial class Score : Label
{
    private Task<ScoreSave> LoadGame;
    private int totalScore;
    private int score = 0;
    private EventBinding<PlanktonEatenEvent> planktonEatenEventBinding;
    private EventBinding<GameLost> gameLostEventBinding;

    public override void _Ready()
    {
        var totalScoreTask = SavingManager.LoadGameAsync<ScoreSave>();
        planktonEatenEventBinding = new EventBinding<PlanktonEatenEvent>(OnPlanktonEatenEvent);
        EventBus<PlanktonEatenEvent>.Register(planktonEatenEventBinding);
        gameLostEventBinding = new EventBinding<GameLost>(OnGameLost);
        EventBus<GameLost>.Register(gameLostEventBinding);
        Text = score.ToString();
        LoadGame = SavingManager.LoadGameAsync<ScoreSave>();
        base._Ready();
    }

    private void OnPlanktonEatenEvent(PlanktonEatenEvent @event)
    {
        score += 1;
        Text = score.ToString();
    }

    private async Task OnGameLost()
    {
        var loadedScore = await LoadGame;
        totalScore = loadedScore?.TotalScore ?? 0;
        await SavingManager.SaveGameAsync(new ScoreSave(totalScore + score));
    }
}
