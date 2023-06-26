
def find_combinations(nums, target):
    # Step 1: Find pairs of integers that sum up to the target
    pairs = []
    seen = set()
    
    for num in nums:
        complement = target - num
        if complement in seen:
            pairs.append([num, complement])
        seen.add(num)
    
    # Step 2: Merge the array into a single array in ascending order
    merged_array = sorted(nums)
    
    # Step 3: Double the target value and find combinations of digits equal to the doubled target
    doubled_target = target * 2
    combinations = []
    
    # Helper function to find combinations using backtracking
    def backtrack(curr_combination, start):
        if sum(curr_combination) == doubled_target:
            combinations.append(curr_combination[:])
        elif sum(curr_combination) > doubled_target:
            return
        elif len(curr_combination) >= len(merged_array):
            return
        else:
            for i in range(start, len(merged_array)):
                curr_combination.append(merged_array[i])
                backtrack(curr_combination, i)
                curr_combination.pop()
    
    # Find combinations using backtracking
    backtrack([], 0)
    
    return pairs, merged_array, combinations

# Test case
nums = [1, 3, 2, 2, -4, -6, -2, 8]
target = 4

pairs, merged_array, combinations = find_combinations(nums, target)

print("First Combination for", target, ":", pairs)
print("Merge into a single array:", merged_array)
print("Second Combination for", target * 2, ":", combinations)
