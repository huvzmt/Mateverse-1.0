import numpy as np
def to_array(str_list):
	for i in range(len(str_list)):
		str_list[i] = float(str_list[i])
	return np.array(str_list)
def to_str(float_list):
	for i in range(len(float_list)):
		float_list[i] = str(float_list[i])
	return float_list

df = open('./data_new.txt','r').readlines()
out = open('./1559.coo','w+')

num_of_atom = len(df)
num_of_bond = num_of_atom//3*2
num_of_angle = num_of_atom//3


out.write('# Saved structures from Unity \n')
out.write(' '.join([str(num_of_atom), 'atoms'])+'\n')
out.write(' '.join([str(num_of_bond), 'bonds'])+'\n')
out.write(' '.join([str(num_of_angle), 'angles'])+'\n')
out.write(' '.join(['2', 'atom', 'types'])+'\n')
out.write(' '.join(['1', 'bond', 'types'])+'\n')
out.write(' '.join(['1', 'angle', 'types'])+'\n')



cell_param = open('./cell.param','r').readlines()[3:]
out.write(' '.join(['-0.12', str(-0.12 + float(cell_param[3])), 'xlo', 'xhi'])+'\n')
out.write(' '.join(['9.936', str(9.936 + float(cell_param[4])), 'ylo', 'yhi'])+'\n')
out.write(' '.join(['-0.353', str(-0.353 + float(cell_param[5])), 'zlo', 'zhi'])+'\n')


out.write('\n')
out.write('Masses \n')
out.write('\n')
out.write('1 15.9994  # O \n')
out.write('2 1.00794  # H \n')


out.write('\n')
out.write('Atoms \n')
out.write('\n')

h_dict, o_dict = {}, {}
pair_dict = {}
alone_oxygen = []

for line in df:
	line_temp = line.split()
	if line_temp[1] == '2':
		h_dict[line_temp[0]] = line_temp[2:]
	elif line_temp[1] == '1' or line_temp[1] == '8':
		o_dict[line_temp[0]] = line_temp[2:]

for oxygen in o_dict.keys():
	o_pos_temp = o_dict[oxygen]
	pair_temp = []
	for hydrogen in h_dict.keys():
		h_pos_temp = h_dict[hydrogen]
		distance_temp = np.linalg.norm(to_array(o_pos_temp) - to_array(h_pos_temp))
		if 0.8 <= distance_temp <= 1.2:
			pair_temp.append([hydrogen, distance_temp])
	pair_temp.sort(key = lambda x:x[1])
	pair = [pair_temp[0][0], pair_temp[1][0]]
	if pair not in pair_dict.values() and pair[::-1] not in pair_dict.values():
		pair_dict[oxygen] = pair
	else:
		alone_oxygen.append(oxygen)

bonded_hydrogen = []
for atoms in pair_dict.values():
	bonded_hydrogen.append(atoms[0])
	bonded_hydrogen.append(atoms[1])

alone_hydrogen = list(set(h_dict.keys())-set(bonded_hydrogen))

for oxygen in alone_oxygen:
	o_pos_temp = o_dict[oxygen]
	pair_temp = []
	for hydrogen in alone_hydrogen:
		pair_temp.append([hydrogen, np.linalg.norm(to_array(o_pos_temp) - to_array(h_pos_temp))])
	pair_temp.sort(key = lambda x:x[1])
	while True:
		idx = 0
		pair = [pair_temp[2*idx][0], pair_temp[2*idx+1][0]]
		if pair not in pair_dict.values() and pair[::-1] not in pair_dict.values():
			pair_dict[oxygen] = pair
			break
		else:
			idx += 1

molecule_count = 1
atom_count = 1
bond_count = 1

for oxygen in pair_dict.keys():
	o_pos = o_dict[oxygen]
	out.write(' '.join([str(atom_count), str(molecule_count), '1', '0.0'] + to_str(o_pos)) + '\n')
	atom_count += 1
	for hydrogen in pair_dict[oxygen]:
		h_pos = h_dict[hydrogen]
		out.write(' '.join([str(atom_count), str(molecule_count), '2', '0.0'] + to_str(h_pos)) + '\n')
		atom_count += 1
	molecule_count += 1

out.write('\n')
out.write('Bonds \n')
out.write('\n')

for i in range(1, num_of_bond//2 +1):
    out.write(' '.join([str(bond_count), '1', str(3*i-2), str(3*i-1)]) + '\n')
    bond_count += 1
    out.write(' '.join([str(bond_count), '1', str(3*i-2), str(3*i)]) + '\n')
    bond_count += 1

out.write('\n')
out.write('Angles \n')
out.write('\n')

for j in range(1, num_of_angle +1):
	out.write(' '.join([str(j), '1', str(3*j-1), str(3*j-2), str(3*j)]) + '\n')


out.close()