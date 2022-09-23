VAR open = false

{open: -> is_open}

#char none
(A ghostly treasure chest prop. It's locked with a 4-phrase padlock.)
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
{open: What a hassle to open.|Great. This one's locked, too.}
-> END

=== is_open ===
#char none
(A ghostly treasure chest prop. It's open.)

-> END
