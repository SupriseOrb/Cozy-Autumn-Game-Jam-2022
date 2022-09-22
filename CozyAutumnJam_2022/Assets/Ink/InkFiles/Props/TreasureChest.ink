VAR open = false

{open: -> is_open}

#char none
(A treasure chest prop. It's locked with a 3-word padlock.)
* [Orange Pumpkin] -> after_choice
* [Clown] -> after_choice
* [Gravestone] -> after_choice
* [Lantern] -> after_choice
* [Pale Pumpkin] -> after_choice 
* [Bat] -> after_choice
* [Purple Pumpkin] -> after_choice
* [Hand] -> after_choice
* [Skull] -> after_choice
* [Brown Pumpkin] -> after_choice

=== after_choice ===
#char cleo
{open: It's finally open.|Who puts an actual lock on a Halloween prop?}
-> END

=== is_open ===
#char none
(A treasure chest prop. It's open.)

-> END
