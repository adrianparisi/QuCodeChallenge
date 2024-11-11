The Wordfinder starts with some validations and the creation of the matrix.
Then to find a wordstream I do several things, first I create the tree with the words that it’s going to be looking for. This tree it’s useful to avoid looping the input list each time that I move from one letter to another and while it’s matching words.
Second are two nested loops to read all the matrix place by place, and on each character it’s searching for possible matches to right and bottom using the tree to match the secuencial letters.
If it finds a match, store in the result dictionary the word and the number of times that was found.
Finally the result it’s sorted to select the ten most common words. On this last point I leave room for a bit of improvements, I can loop just once if I do not use LinQ but as the Take() and Select() are only with 10 items each there should not be a huge performance issue.
