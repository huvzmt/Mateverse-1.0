import sys
import time

path = sys.argv[1]
ff_original = open(path, "r").readlines()

flags = [0, 0]
## partition the input force field file
for line in range(len(ff_original)):
    if ff_original[line].startswith("pair_style") and flags[0] == 0:
        style_index = line
        flags[0] = 1
        head  = ff_original[:style_index]
        continue
    elif ff_original[line].startswith("pair_coeff") and flags[1] == 0:
        coeff_index = line
        flags[1] = 1
        style = ff_original[style_index:coeff_index]
        continue
    elif ff_original[line].startswith("set type"):
        charge_index = line
        coeff = ff_original[coeff_index:charge_index]
        rest  = ff_original[charge_index:]
        break

forcefields = sys.argv[2:]
num_ff      = len(forcefields)
## each input arg refers to one force field in the format as
## 0.5+lj/cut/coul/long+10.0=1-1~0.000-0.000_1-2~0.000-0.000_2-2~0.102-3.188

# if num_ff > 1:
# if True:
pair_style = "pair_style hybrid/overlay "
pair_style += " ".join([forcefields[i].split("=")[0].replace("+", " ") for i in range(num_ff)]) + "\n"
## the force fields are seperated via "="

pair_coeffs = []
for i in range(num_ff):
    temp_ff     = forcefields[i].split("=")[0].split("+")[0] ## [1] if scaled
    temp_coeffs = forcefields[i].split("=")[1].split("_")
    for j in temp_coeffs:
        temp_pairs, temp_parameters = j.split("~")
        if temp_ff == "hbond/dreiding/lj":
            pair_coeffs.append(
                "pair_coeff " + temp_pairs.replace("-", " ") + " " + temp_ff
                + " " + "1 i"
                ## denoting the donor and acceptor for the H bond.
                + " " + temp_parameters.replace("-", " ") + "\n")
        else:
            pair_coeffs.append(
                "pair_coeff " + temp_pairs.replace("-", " ") + " " + temp_ff
                + " " + temp_parameters.replace("-", " ") + "\n")
# else:
#     pair_style = "pair_style "
#     pair_style += " ".join([forcefields[i].split("=")[0].replace("+", " ") for i in range(num_ff)]) + "\n"
#     ## the force fields are seperated via "="
#
#     pair_coeffs = []
#     for i in range(num_ff):
#         temp_ff = forcefields[i].split("=")[0].split("+")[0] ## [1] if scaled
#         temp_coeffs = forcefields[i].split("=")[1].split("_")
#         for j in temp_coeffs:
#             temp_pairs, temp_parameters = j.split("~")
#             if temp_ff == "hbond/drieding/lj":
#                 pair_coeffs.append(
#                     "pair_coeff " + temp_pairs.replace("-", " ") + " " + temp_ff
#                     + " " + "1 i"
#                     ## denoting the donor and acceptor for the H bond.
#                     + " " + temp_parameters.replace("-", " ") + "\n")
#             else:
#                 pair_coeffs.append(
#                     "pair_coeff " + temp_pairs.replace("-", " ") + " " + temp_ff
#                     + " " + temp_parameters.replace("-", " ") + "\n")

new_lines = "".join(head + [pair_style] + pair_coeffs
                    ##+ ["pair_modify tail yes\n", "kspace_style pppm 1.0e-5\n"]
                    + ["kspace_style pppm 1.0e-5\n"]
                    + ["\n"] + rest)

with open(path, "w") as f:
    f.write(new_lines)
    f.close()

with open("record_pair styles.txt", "a") as f:
    f.write(" ".join(sys.argv[2:]) + "\n")
    f.close()