namespace CSC_317_MAUI_Example_Guessing_Game;

public partial class MainPage : ContentPage
{
	const string up_arrow = "↑";
	const string down_arrow = "↓";
	const int num_guesses = 10;
	int remaining_guesses = num_guesses;
	int com_number;
	Random r = new Random();
	bool restart = false;

	public MainPage()
	{
		InitializeComponent();
		StartGame();
	}

	private void StartGame()
	{
		//Start a new game.
		com_number = r.Next(0, 100);

		txtInput.Text = String.Empty;
		lblArrow.Text = "---";
		lblHint.Text = "";
		remaining_guesses = num_guesses;
		lblGuessesLeft.Text = $"Remaining Guesses: {remaining_guesses}/{num_guesses}";
		restart = false;
	}


	private void EndGame(bool win)
	{
		//Setup the end game screen.
		if(win == true)
		{
			lblArrow.Text = "";
			lblHint.Text = "You won!";
			lblGuessesLeft.Text = "";
			
		}
		else if(win == false)
		{

			lblArrow.Text = "";
			lblHint.Text = "You Lost...";
			lblGuessesLeft.Text = $"The number was {com_number}";
		}

		restart = true;
		btnGuess.Text = "Restart Game";
	}

	private void ProcessGuess()
	{
		//Proccess a user's guess.
		int user_guess;
		try
		{
			//Attempt to convert the entered input into a number.
			user_guess = Convert.ToInt32(txtInput.Text);
		}
		catch(FormatException)
		{
			//If the conversion fails, then set an error message on
			//the hints label, reset the arrow, and don't do anything
			//else in this function.
			lblHint.Text = "This is not a number.  Enter again...";
			lblArrow.Text = "---";
			return;
		}

		//If the number was successfully converted, then you may proceed.
		if (user_guess == com_number)
			EndGame(true);
		else
		{
			//Tell the user to guess higher.
			if(user_guess < com_number)
			{
				lblArrow.Text = up_arrow;
				lblHint.Text = "You should guess higher...";

			}
			//Tell the user to guess lower.
			else
			{
				lblArrow.Text = down_arrow;
				lblHint.Text = "You should guess lower...";
			}

			remaining_guesses--;
			lblGuessesLeft.Text = $"Remaining Guesses: {remaining_guesses}/{num_guesses}";

			if (remaining_guesses == 0)
				EndGame(false);
		}

	}

	private void OnGuessClick(object sender, EventArgs e)
	{
		//Guess the number or restart game.

		//If the user clicks the button after a game is over,
		//restart the game.
		if (restart == true)
		{
			StartGame();
		}
		else
		{
			ProcessGuess();
		}
	}
}

