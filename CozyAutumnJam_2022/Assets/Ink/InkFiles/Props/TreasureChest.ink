VAR open = false

{open: -> is_open}

#char none
(A treasure chest prop. It's locked with a 3-digit padlock.)
* [0] -> after_choice
* [1] -> after_choice
* [2] -> after_choice
* [3] -> after_choice 
* [4] -> after_choice
* [5] -> after_choice
* [6] -> after_choice
* [7] -> after_choice
* [8] -> after_choice
* [9] -> after_choice

=== after_choice ===
#char cleo
{open: It's finally open.|Who puts an actual lock on a Halloween prop?}
-> END

=== is_open ===
#char none
(A treasure chest prop. It's open.)

-> END
