import System.IO

part1 = do
    s <- readFile "../inputs/day1"
    let x = [ read [s !! x] :: Int | x <- [0..(length s - 1)], s !! x == s !! ( if x + 1 > (length s - 1) then x - length s + 1 else x + 1)]
    print (sum x)
    
