import System.IO
import Text.Printf

main = do
    s <- readFile "../inputs/day2"
    putStrLn ("Part 1: " ++ show (checksum' s))

checksum :: String -> Int
checksum s =
    let splits = [[read i :: Int | i <- words line] | line <- lines s]
        rows = [maximum s - minimum s | s <- splits]
    in sum rows        

-- same thing but in where style vs let style
checksum' :: String -> Int
checksum' s = 
    sum [maximum x - minimum x | x <- splits]
    where splits = [[read i :: Int | i <- words line] | line <- lines s]
