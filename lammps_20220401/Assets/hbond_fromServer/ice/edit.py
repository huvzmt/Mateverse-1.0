df_xyz = open('./1559.data', 'r', encoding='utf-8').readlines()[18:114]
df_bond = open('./1559.data', 'r', encoding='utf-8').readlines()[117:181]
out = open('./1559.xyz','w+')



# for line in df:
#     line_temp = line.split()
#     if line_temp[2] == '8':
#         line_temp[2] = '2'
#     out.write(' '.join(line_temp)+'\n')
#
# out.close()

pair_dict = {}
for atom in df_xyz:
    if atom.split()[2] == '8':
        pair_dict[atom.split()[0]] = []


for line in df_bond:
    line_temp = line.split()
    pair_dict[line_temp[3]].append(line_temp[2])

# print(pair_dict)
molecule_count = 1
atom_count = 1

for id in pair_dict.keys():
    o_pos = df_xyz[int(id)-1].split()[4:]
    out.write(' '.join([str(atom_count), str(molecule_count), '1', '0.0'] + o_pos) + '\n')
    atom_count += 1
    for h in pair_dict[id]:
        h_pos = df_xyz[int(h)-1].split()[4:]
        out.write(' '.join([str(atom_count), str(molecule_count), '2', '0.0'] + h_pos) + '\n')
        atom_count += 1
    molecule_count += 1

bond_out = open('./bond.txt','w+')
angle_out = open('./angle.txt','w+')

bond_count = 1
for i in range(1, len(df_bond)//2 +1):
    bond_out.write(' '.join([str(bond_count), '1', str(3*i-2), str(3*i-1)]) + '\n')
    bond_count += 1
    bond_out.write(' '.join([str(bond_count), '1', str(3*i-2), str(3*i)]) + '\n')
    bond_count += 1

    angle_out.write(' '.join([str(i), '1', str(3*i-1), str(3*i-2), str(3*i)]) + '\n')
bond_out.close()
angle_out.close()