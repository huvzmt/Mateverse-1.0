# import sys
# import time

# path = sys.argv[1]
# elem_id = sys.argv[2]
# elem_x = sys.argv[3]
# elem_y = sys.argv[4]
# elem_z = sys.argv[5]
# data_original = open(path, "r").readlines()

# for line in range(len(data_original)):
#     if len(data_original[line]) == 10 and data_original[line].startswith(elem_id):
#         line_str = data_original[line].split(" ")
#         line_str[4] = elem_x
#         line_str[5] = elem_y
#         line_str[6] = elem_z
#         data_original[line] = " ".join(line_str)
#         break
#     end
# end

# with open(path, "w") as f:
#     f.write(data_original)
#     f.close



import sys
import time

newfilepath = sys.argv[1]
updatefilefile = sys.argv[2]
new_data = open(newfilepath, "r").readlines()
data_original = open(updatefilefile, "r").readlines()

start_line = 0
for line in range(len(data_original)):
    start_line++;
    if len(data_original[line]) == 10 and data_original[line].startswith(elem_id):
        line_str = data_original[line].split(" ")
        line_str[4] = elem_x
        line_str[5] = elem_y
        line_str[6] = elem_z
        data_original[line] = " ".join(line_str)
        break
    end
end



with open(updatefilefile, "w") as f:
    f.write(data_original)
    f.close
