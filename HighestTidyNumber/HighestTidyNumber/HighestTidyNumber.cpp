// HighestTidyNumber.cpp : Defines the entry point for the console application.
//

#include <iostream>
#include <fstream>
#include <string>
#include <sstream>

using namespace std;

//TDD
bool checkIfCorrect(string input, string result)
{
	int rlen = result.length();
	int ilen = input.length();

	int index = 0;
	if (ilen - rlen > 1) {
		cout << "Failed ilen - rlen > 1 in=" << input << ",  res="<<result <<endl; 
		return false;
	}

	if (rlen > ilen  )
	{
		cout << "Failed rlen > ilen in=" << input << ",  res="<<result <<endl; 
		return false;
	}
	
	else if (rlen < ilen) {
		if (result[0] > input[0])
		{
			//cout << "Failed rlen < ilen && result[0] > input[0] in=" << input << ",  res="<<result <<endl; 
			return true; //only exception... no way to prove if can be compared..ignore and rely on length
		}
	}
	else {
		while (rlen > index)
		{
			if (result[index] == input[index])
				++index;
			else if (result[index] > input[index])
			{
				cout << "Failed ilen == rlen &&  result[index] > input[index] in=" << input << ",  res="<<result <<endl; 
				return false;
			}
			else
			{
				return true;
			}
		}
	}

	return true;
}

string outpath = ".\\..\\Debug\\";

bool readBigInputs(string fname)
{
	string ifilepath = outpath + fname+ ".in";
	string ofilepath = outpath + fname+ ".out";

	std::ofstream ofs(ofilepath, std::ofstream::out);

	std::ifstream file(ifilepath);
	if (!file){
		ofs.close();
		cout << "Input file not found at input path.\n";
		return false;
	}

	if (file.is_open())
	{
		cout << "Input file opened successfully.\n";
		std::string line;
		int testCaseId = 0;
		string str = "";
		string::size_type sz;

		getline(file, str);
		int testcases = std::stoi(str, &sz);

		int caseid = 1;
		while (getline(file, line))
		{
			//cout << line << endl;

			bool tidy = false;
			//while (false == tidy)
			{
				int sz = line.size();
				int counter = 0;

				if (sz == 1) {
					tidy = true;
					//cout << "this is tidy " << line << endl;
					ofs << "Case #" << caseid++ << ": " << line << endl;
					continue;
				}

				string newline = "";
				int totalappendcount = 0;

				while (newline.size() <= sz  )
				{
					if (counter == sz - 1)
					{
						newline += line[counter];
						break;
					}
					if (line[counter] <= line[counter + 1]) {
						newline += line[counter];
						++counter;
					}
					else {
						//find the immediate smaller number
						char p = line.at(counter);
						int number = atoi(&p);
						--number;
						
						char buff [2]="\0";
						itoa(number, buff, 10 );

						//traverse backwards to find the smaller number then the new smaller number.
						int rcounter = 0;
						int newlinesz = newline.length() ;
						int lastval = 0;

						if (newlinesz > 0)
						{
							int lastNewlineIndex = newlinesz - 1;
							do{
								p = newline.at(lastNewlineIndex - rcounter);
								lastval = atoi(&p);

								if (lastval <= number)
									break;
								++rcounter;
							} while ( (lastNewlineIndex - rcounter) >= 0 );


							if((newlinesz - rcounter)==0)//if 0 index to be updated, 
								totalappendcount = sz - 1;
							else							
								totalappendcount = sz - (newlinesz - rcounter);

							//append smaller number and fill with 9
							if (newlinesz - rcounter > 0)
							{
								newline = newline.substr(0, newlinesz - rcounter);
								newline += buff;
								--totalappendcount;
							}
							else
							{
								newline = "";
								if (number > 0) {
									newline = buff;
									//--totalappendcount;
								}

							}
						}
						else
						{
							
							if (number > 0) {
								newline = buff;
								totalappendcount = line.length() -1;
							}
							else
							{
								totalappendcount = line.length() - 1;
							}


						}
						for (int x = 0; x < totalappendcount;++x) {
							newline += "9";
						}

						break;

					}
				}

				cout << "Case #" << caseid << ": "<< line <<"\n" << newline << endl<<endl;
				ofs << "Case #" << caseid++ << ": " << newline << endl;
				checkIfCorrect(line, newline);
				

			}
		}

	}

	ofs.close();
	file.close();

	return true;
}


bool readInputs(string fname)
{

	string ifilepath = outpath + fname + ".in";
	string ofilepath = outpath + fname + ".out";

	std::ofstream ofs(ofilepath, std::ofstream::out);

	std::ifstream file(ifilepath);
	if (!file)
		return false;

	if (file.is_open())
	{
		cout << "file opened successfully.";
		std::string line;
		int testCaseId = 0;
		string str = "";
		string::size_type sz;

		getline(file, str);
		int testcases = std::stoi(str, &sz);

		int caseid =1;
		while (getline(file, line))
		{
			cout << line << endl;
			//int sz = line.size();
			//int counter = sz-1;
			bool tidy = false;
			while (false == tidy) 
			{
				int sz = line.size();
				int counter = sz - 1;

				if (sz == 1) {
					tidy = true;
					cout << "this is tidy " << line << endl;
					ofs << "Case #"<< caseid++ << ": " << line << endl;
					continue;
				}

				while (/*line.size() > sz - counter && */counter-1 >= 0)
				{
					if (line[counter] >= line[counter - 1]) {
						counter--;
					}
					else
						break;

					//counter -= 1;
				}
				if (counter == 0)
				{
					cout << "this is tidy " << line << endl;
					ofs << "Case #" << caseid++ << ": " << line << endl;
					tidy = true;
				}
				else
				{
					string::size_type sz;
					//uint64_t number = string_to_uint64(line);
					int number = std::stoi(line, &sz);;
					number--;
					line = std::to_string(number);
					
				}
			}
		}

	}

	ofs.close();
	file.close();

}



int main()
{


	readBigInputs("BL");

	readBigInputs("BS");

    return 0;
}

