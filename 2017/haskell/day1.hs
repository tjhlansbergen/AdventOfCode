import System.IO

part1 = do
    s <- readFile "../inputs/day1"
    putStrLn (show  [read (s !! x) :: Int | x <- [0..((length s)-1)], s !! x == s !! ( if x + 1 > ((length s) -1) then 0 else x + 1)])
    putStrLn s
  