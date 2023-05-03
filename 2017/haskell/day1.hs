import System.IO
import Text.Printf

main = do
    s <- readFile "../inputs/day1"
    putStrLn ("Part 1: " ++ show (countEqual s 1))
    putStrLn ("Part 2: " ++ show (countEqual s (length s `div` 2)))

countEqual :: String -> Int -> Int
countEqual s ahead =
    sum x
    where x = [ read [s !! x] :: Int | x <- [0..(length s - 1)], s !! x == s !! ( if x + ahead > (length s - 1) then x - length s + ahead else x + ahead)]

