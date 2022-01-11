import helpers
import sys

lines = helpers.list_int_from_file('2020/inputs/day1')


for i in range(len(lines)):
    # go through list top down
    needed_a = 2020 - lines[i]
    
    # go throug the list again, starting at our current permission (since it addition order doesnt matter, just need to visit ech combo once)
    for j in range(i, len(lines)):
        needed_b = needed_a - lines[j]

        # and again, at the current position
        for h in range(j, len(lines)):
            if(lines[h] == needed_b):
                print(lines[i]*lines[j]*lines[h])
                sys.exit()
            
    del lines[i]     # that one yielded no results, skip it in future iterations



        


