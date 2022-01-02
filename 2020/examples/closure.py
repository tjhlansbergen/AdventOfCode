def multiplier(by: int):
    def multiply(x: int):
        return x*by
    return multiply


times_3 = multiplier(3)
times_5 = multiplier(5)

print(times_3(5))   #15
print(times_5(5))   #25