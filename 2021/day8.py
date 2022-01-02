import helpers

rows = helpers.list_from_file('inputs/day8')


# part one

# segments for digits 1, 4, 7, 8
segments_sizes = [2,4,3,7]

# for each row, split on | then on space, count and compare each digits lenght, count matches per row and sum rowcounts
print(sum([len([digit for digit in row.split("|")[1].split(" ") if len(digit) in segments_sizes]) for row in rows]))


# part two ##############################################################################################################

#  aaa
#b      c
#b      c
#  ddd
#e      f
#e      f
#  ggg
#

# len 2 = 1
# len 3 = 7
# len 4 = 4
# len 5 = 2, 3, 5
# len 6 = 0, 6, 9
# len 7 = 8

# this needs some refactoring but for now it works
def get_encoding(digits):
    segments = {"a": "abcdefg", "b": "abcdefg", "c": "abcdefg", "d": "abcdefg", "e": "abcdefg", "f": "abcdefg", "g": "abcdefg"}

    for digit in digits:
        if len(digit) == 2: # 1 
            # c & f are any of the two chars in this digit 
            segments["c"] = "".join([x for x in segments["c"] if x in digit])
            segments["f"] = "".join([x for x in segments["f"] if x in digit])
            # all others are not any of the chars in this digit    
            segments["a"] = "".join([x for x in segments["a"] if x not in digit])
            segments["b"] = "".join([x for x in segments["b"] if x not in digit])
            segments["d"] = "".join([x for x in segments["d"] if x not in digit])
            segments["e"] = "".join([x for x in segments["e"] if x not in digit])
            segments["g"] = "".join([x for x in segments["g"] if x not in digit])

        if len(digit) == 3: # 7
            # a c & f are the chars in this digit
            segments["a"] = "".join([x for x in segments["a"] if x in digit])
            segments["c"] = "".join([x for x in segments["c"] if x in digit])
            segments["f"] = "".join([x for x in segments["f"] if x in digit])
            # the others are not    
            segments["b"] = "".join([x for x in segments["b"] if x not in digit])
            segments["d"] = "".join([x for x in segments["d"] if x not in digit])
            segments["e"] = "".join([x for x in segments["e"] if x not in digit])
            segments["g"] = "".join([x for x in segments["g"] if x not in digit])

        if len(digit) == 4: # 4
            # b d c & f are the chars in this digit
            segments["b"] = "".join([x for x in segments["b"] if x in digit])
            segments["d"] = "".join([x for x in segments["d"] if x in digit])
            segments["c"] = "".join([x for x in segments["c"] if x in digit])
            segments["f"] = "".join([x for x in segments["f"] if x in digit])
            # the others are not    
            segments["a"] = "".join([x for x in segments["a"] if x not in digit])
            segments["e"] = "".join([x for x in segments["e"] if x not in digit])
            segments["g"] = "".join([x for x in segments["g"] if x not in digit])

        if len(digit) == 5: # 2, 3, 5
            # chars NOT in here will be at b, c, e or f
            segments["a"] = "".join([x for x in segments["a"] if x in digit])
            segments["d"] = "".join([x for x in segments["d"] if x in digit])
            segments["g"] = "".join([x for x in segments["g"] if x in digit])

        if len(digit) == 6: # 0, 6, 9
            # chars NOT in here will be at c, d, e
            segments["a"] = "".join([x for x in segments["a"] if x in digit])
            segments["b"] = "".join([x for x in segments["b"] if x in digit])
            segments["f"] = "".join([x for x in segments["f"] if x in digit])
            segments["g"] = "".join([x for x in segments["g"] if x in digit])

        single_segment_values = [x for x in segments.values() if len(x) == 1]
        if len(single_segment_values) > 1:
            for k, v in segments.items():
                if len(v) > 1:
                    segments[k] = "".join([x for x in v if x not in single_segment_values])
            
    # invert the mapping
    return {v: k for k, v in segments.items()}



def digitalize(segments, encoding):
    if len(segments) == 2:
        return 1
    if len(segments) == 3:
        return 7
    if len(segments) == 4:
        return 4
    if len(segments) == 5:
        segments = [encoding[x] for x in segments]
        if "b" not in segments and "f" not in segments:
            return 2
        if "b" not in segments and "e" not in segments:
            return 3
        if "c" not in segments and "e" not in segments:
            return 5
    if len(segments) == 6:
        segments = [encoding[x] for x in segments]
        if "d" not in segments:
            return 0
        if "c" not in segments:
            return 6
        if "e" not in segments:
            return 9
    if len(segments) == 7:
        return 8


results = []

for row in rows:
    digits = row.split(" | ")[0].split(" ")
    outputs = row.split(" | ")[1].split(" ")
    encoding = get_encoding(digits)

    digits = [digitalize(segments, encoding) for segments in outputs]
    result = int(''.join(str(i) for i in digits))
    results.append(result)

print(sum(results))


