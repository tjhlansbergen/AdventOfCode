from typing import List
import math

def liebnitz_pi(terms: int) -> float:
    denominator = 1
    pi = 0

    for i in range(terms):
        if i % 2 == 0:
            pi += 4/denominator
        else:
            pi -= 4/denominator
        denominator += 2
    #print(pi)
    return pi

# get pi once
pi = math.pi
strpi = str(pi)[2:]
count = 1

for i in range(1,str(pi)[::-1].find('.')):
    found = False
    
    while found == False:
        found = strpi[:i] == str(liebnitz_pi(count))[2:][:i]
        count+=count
    print(i, count)
