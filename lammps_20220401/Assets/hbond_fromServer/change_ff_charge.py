import sys

path = sys.argv[1]
ff_original = open(path, "r").readlines()

flags = [0]
## partition the input force field file
for line in range(len(ff_original)):
    if ff_original[line].startswith("set type") and flags[0] == 0:
        charge_index = line
        flags[0] = 1
        head  = ff_original[:charge_index]
        continue
    elif ff_original[line].startswith("bond_style"):
        bonds_index  = line
        charge= ff_original[charge_index:bonds_index]
        rest  = ff_original[bonds_index:]
        break

charge_args = sys.argv[2:]
## each input arg refers to an assignment to the charge
## the argument is expected in the format of 1_0 for type 1 and charge 0

charges = []
for i in range(len(charge_args)):
    temp = charge_args[i].split("_")
    charges.append("set type " + temp[0] + " charge " + temp[1] + "\n")
charges.append("\n")

new_lines = "".join(head + charges + rest)

with open(path, "w") as f:
    f.write(new_lines)
    f.close()

with open("record_charges.txt", "a") as f:
    f.write(" ".join(sys.argv[2:]) + "\n")
    f.close()