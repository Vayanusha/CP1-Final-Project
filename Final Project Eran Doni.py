import random
balance = 0
num = random.randint(0,49)
countries = ["Belgium", "Iran", "Madagascar", "Mexico",  "Venezuela", "Nigeria",  "Albania", "Vietnam", "Singapore", "Armenia", "Sri Lanka", "Syria", "Kenya", "Norway", "Spain", "Oman", "Argentina", "Croatia", "Laos", "North Korea", "Portugal", "Saudi Arabia", "New Zealand", "Uzbekistan", "Netherlands", "Algeria", "Czech Republic", "Panama", "Uganda", "Canada", "South Korea", "Zimbabwe", "Ecuador", "Vatican City", "Chad", "Egypt", "Poland", "Uruguay", "Cambodia", "Germany", "Indonesia", "United States of America", "Morocco", "Costa Rica", "Paraguay", "India", "Sweden", "Antarctica", "Italy", "Israel"]
country = countries[num]
name = input("What is your name?")
happiness = 100
health = 100
smarts = random.randint(1, 100)
looks = random.randint(1,100)
occupation = "None"
relationships = []
assets = []

maleNames = ["James", "John", "Robert", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Donald", "Mark", "Paul", "Steven", "Andrew", "Kenneth"]
femaleNames = ["Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah", "Margaret", "Karen", "Nancy", "Lisa", "Betty", "Dorothy", "Sandra", "Ashley", "Kimberly", "Donna", "Emily"]
lastNames = ["Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson"]

class Person:
    def __init__(self, name = "", age = 0, relationship = 50, gender = ""):
        self.name = name
        self.age = age
        self.relationship = relationship
        self.gender = gender
    def __str__(self):
        return "({0}, {1}, {2}, {3})".format(self.name, self.age, self.relationship, self.gender)
def chooseName(gender):
    num = random.randint(0,19)
    if gender == "Male":
        return maleNames[num]
    if gender == "Female":
        return femaleNames[num]
    else:
        return lastNames[num]

last = ""
last = chooseName("")

mom = Person((chooseName("Female") + " " + last), random.randint(18,69), 100, "Female")
dad = Person((chooseName("Male") + " " + last), random.randint(18,69), 100, "Male")

relationships.append(mom)
relationships.append(dad)

occupation = ""

jobAction = False
yearsInCollege = 0

print("My name is " + name + " " + last)
print("I was born in " + country)
print("My dad is " + dad.name + ". He is " + str(dad.age))
print("My mom is " + mom.name + ". She is " + str(mom.age))
print("Happiness: " + str(happiness))
print("Health: " + str(health))
print("Smarts: " + str(smarts))
print("Looks: " + str(looks))

meAge = 0
education = "None"

def menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege):
    print("1: Age")
    print("2: Occupation")
    print("3: Relationships")
    print("4: Activities")
    print("5: Assets (does not fully work yet)")
    choice = (input(""))
    if choice == "":
        print("Not a valid option")
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    else:
        choice = int(choice)
    if choice == 1:
        meAge, occupation , smarts, happiness, education, balance, yearsInCollege = age(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    elif choice == 2:
        occupation = Job(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    elif choice == 3:
        relationships = people(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    elif choice == 4:
        relationships, balance, happiness = activities(balance, happiness)
    elif choice == 5:
        assets, balance = asset(assets, balance)
    else:
        print("Not a valid option")
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
def age(myAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege):
    myAge += 1
    print("I am " + str(myAge) + " years old")
    print("Happiness: " + str(happiness))
    print("Health: " + str(health))
    print("Smarts: " + str(smarts))
    print("Looks: " + str(looks))
    print("Money: $" + str(balance))
    for i in range(len(relationships)):
        relationships[i].age += 1
    for i in range(len(relationships)):
        if relationships[i].age == 100:
            print(relationships[i].name + " has died")
            relationships.pop(i)
    if myAge == 6:
        print("You started elementary school")
        occupation = "Elementary School"
        print(occupation)
    elif myAge == 11:
        print("You started middle school")
        occupation = "Middle School"
    elif myAge == 14:
        print("You started high school")
        occupation = "High School"
    elif myAge == 18 and occupation == "High School":
        print("You finished high school")
        occupation = "Unemployed"
        education = "High School"
    elif occupation == "Low Paying Job":
        balance += 15000
    elif occupation == "Medium Paying Job":
        balance += 20000
    elif occupation == "High Paying Job":
        balance += 40000
    elif occupation == "College":
        yearsInCollege += 1
        print(str(yearsInCollege))
        if yearsInCollege == 4:
            print("You finished College")
            occupation = "Unemployed"
            education = "College"
    if myAge == 100:
        die()
    return (myAge, occupation, smarts, happiness, education, balance, yearsInCollege)

def die():
    print("You died")
    die2()
def die2():
    print("You died")
    die3()
def die3():
    print("You died")
    die4()
def die4():
    print("You died")
    die5()
def die5():
    print("You died")
    die6()
def die6():
    print("You died")
    die7()
def die7():
    print("You died")
    die8()
def die8():
    print("You died")
    die9()
def die9():
    print("You died")
    die10()
def die10():
    print("You died")
    die()
def Job(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege):
    if jobAction == False:
        if occupation == "":
            print("Children cannot have an occupation")
            menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
        elif occupation == "Elementary School" or occupation == "Middle School":
            print("1: Study Harder")
            print("2: Drop Out")
            print("3: Go back")
            choice = int(input(""))
            if choice == 1:
                print("You started studying harder in school")
                smarts += 4
                happiness -= 10
                print("Smarts: " + str(smarts))
                print("Hapiness: " + str(happiness))
                if happiness < 0:
                    print("You died from unhappiness")
                    die()
                elif smarts > 100:
                    print("You died from being too smart. Your brain was so large your head exploded.")
                    die()
                else:
                    menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            elif choice == 2:
                print("Nice try. Your parents didn't let you drop out of school")
                menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            elif choice == 3:
                menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            else:
                print("Not a valid option")
                Job(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
        if occupation == "High School":
            print("1: Study Harder")
            print("2: Drop Out")
            print("3: Go back")
            choice = int(input(""))
            if choice == 1:
                print("You started studying harder in school")
                smarts += 4
                happiness -= 10
                if smarts > 100:
                    smarts = 100
                print("Smarts: " + str(smarts))
                print("Hapiness: " + str(happiness))
                if happiness < 0:
                    print("You died from unhappiness")
                elif smarts > 100:
                    print("You died from being too smart. Your brain was so large your head exploded.")
                else:
                    menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            elif choice == 2:
                if smarts <= 30:
                    print("You dropped out of high school.")
                    occupation = "Unemployed"
                    menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                elif smarts >= 30:
                    print("You are too smart to drop out of high school.")
                    menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                jobAction = True
            elif choice == 3:
                menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            else:
                print("Not a valid option")
                Job(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
        if occupation == ("Unemployed"):
            print("1: Job")
            print("2: Education")
            print("3: Go back")
            choice = int(input(""))
            if choice == 1:
                print("1: Grocery Worker")
                print("2: Fast Food Worker")
                print("3: Janitor")
                print("4: Cashier")
                print("5: Security Guard")
                print("6: Landscaper")
                print("7: Gardener")
                print("8: Waiter")
                print("9: Mailman")
                print("10: Policeman")
                print("11: Tour Guide")
                print("12: Factory Worker")
                print("13: Teacher")
                print("14: Plumber")
                print("15: Engineer")
                print("16: Dentist")
                print("17: Stockbroker")
                print("18: Architect")
                print("19: Veterenerian")
                print("20: Lawyer")
                choice = int(input(""))
                if choice >= 1 and choice <= 8:
                    print("Requirements: None")
                    print("Salary: 30000")
                    print("1: Apply for this position")
                    print("2: Go back")
                    choice = int(input(""))
                    if choice == 1:
                        print("You got the job")
                        occupation = "Low Paying Job"
                        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                    elif choice == 2:
                        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                    else:
                        print("Not a valid option")
                        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                elif choice >= 9 and choice <= 12:
                    print("Requirements: High School")
                    print("Salary: 40000")
                    print("1: Apply for this position")
                    print("2: Go back")
                    choice = int(input(""))
                    if choice == 1:
                        if education == "High School" or education == "College":
                            print("You got the job")
                            occupation = "Medium Job"
                            menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                        else:
                            print("Your level of education is not high enough for this job")
                            menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                    if choice == 2:
                        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                    else:
                        print("Not a valid option")
                        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                elif choice >= 13 and choice <= 20:
                    print("Requirements: College")
                    print("Salary: 80000")
                    print("1: Apply for this position")
                    print("2: Go back")
                    choice = int(input(""))
                    if choice == 1:
                        if education == "College":
                            print("You got the job")
                            occupation = "High Paying Job"
                            menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                        else:
                            print("Your level of education is not high enough for this job")
                            menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                    if choice == 2:
                        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                    else:
                        print("Not a valid option")
                        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            if choice == 2:
                print("1: Apply to College")
                print("2: Go back")
                choice = int(input(""))
                if choice == 1:
                    if smarts >= 50:
                        print("How will you pay for college?")
                        print("1: Apply for a scholarship")
                        print("2: Apply for a student loan")
                        choice = int(input(""))
                        if choice == 1:
                            if smarts >= 80:
                                print("You got a scholarship!")
                                occupation = "College"
                                menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                            else:
                                print("You didn't get a scholarship and took a loan")
                                balance -= 60000
                                occupation = "College"
                                menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                        if choice == 2:
                            print("You took a student loan")
                            occupation = "College"
                            balance -= 60000
                            menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                    elif smarts < 50:
                        print("You did not get into college")
                        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
                if choice == 2:
                    menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            if choice == 3:
                menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
        elif occupation == "Low Paying Job" or occupation == "Medium Paying Job" or occupation == "High Paying Job":
            print("1: Search for a new job")
            print("2: Retire")
            print("3: Go back")
            choice = int(input(""))
            if choice == 1:
                occupation = "Unemployed"
                Job(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            elif choice == 2:
                occupation = "Unepmloyed"
                menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            elif choice == 3:
                menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            else:
                print("Not a valid option")
                menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
        elif occupation == "College":
            print("1: Drop out of college")
            print("2: Go back")
            choice = int(input(""))
            if choice == 1:
                occupation = "Unemployed"
                menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            elif choice == 2:
                menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
            else:
                print("Not a valid option")
                Job(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    else:
        print("Already did something today")
    return occupation

def people(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege):
    if len(relationships) == 0:
        print("You don't know anyone")
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    for i in range(len(relationships)):
        print(str(i + 1) + ": " + relationships[i].name)
    choice = int(input(""))
    print("Relationship status with this person: " + str(relationships[choice - 1].relationship))
    print("1: Improve relationship")
    print("2: Ask for money")
    print("3: Go back")
    choice2 = int(input(""))
    if choice2 == 1:
        relationships[choice - 1].relationship += 10
        if relationships[choice - 1].relationship > 100:
            relationships[choice - 1].relationship = 100
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    elif choice2 == 2:
        if relationships[choice - 1].relationship > 20:
            relationships[choice - 1].relationship -= 20
            balance += random.randint(1, 100)
            menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
        else:
            print(relationships[choice - 1].name + " refused to give you money.")
            menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    elif choice2 == 3:
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    return relationships
def activities(balance, happiness):
    print("1: Meet a new person")
    print("2: Watch a movie")
    print("3: Lottery")
    print("4: Go on vacation")
    print("5: Die")
    print("6: Go back")
    choice = int(input(""))
    if choice == 1:
        n = random.randint(0,10)
        if n % 2 == 1:
            f = Person((chooseName("Female") + " " + chooseName("")), random.randint(0,69), 50, "Female")
            relationships.append(f)
            print("f")
        elif n % 2 == 0:
            m = Person((chooseName("Male") + " " + chooseName("")), random.randint(0,69), 50, "Male")
            relationships.append(m)
            print("m")
        else:
            print("?")
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    elif choice == 2:
        if balance >= 15:
            print("You went to see a movie")
            balance -= 15
            happiness += 10
            if happiness > 100:
                happiness = 100
        elif balance < 15:
            print("You don't have enough money to go see a movie")
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    elif choice == 3:
        if balance >= 10:
            n = random.randint(0, 10000)
            if n <= 10:
                print("You won the lottery!")
                balance += 100000000000
            elif n > 10:
                print("You didn't win the lottery. What a surprise.")
        elif balance < 10:
            print("You don't have enough money to buy a lottery ticket.")
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    elif choice == 4:
        if balance >= 5000:
            print("You went on a fun vacation")
            happiness += 50
            balance -= 5000
        elif balance < 5000:
            print("You don't have enough money to go on vacation")
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    elif choice == 5:
        die()
    elif choice == 6:
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    return relationships, balance, happiness

def asset(assets, balance):
    print("1: Buy real estate")
    print("2: Buy a car")
    print("3: Manage assets")
    print("4: Go back")
    choice = int(input(""))
    if choice == 1:
        print("1: Buy a a small house")
        print("2: Buy a medium house")
        print("3: Buy a large house")
        choice = int(input(""))
        if choice == 1:
            if balance >= 50000:
                print("You bought a small house for $50000")
                assets.append("Small house")
            elif balance < 50000:
                print("You don't have enough money to buy a small house")
        elif choice == 2:
            if balance >= 100000:
                print("You bought a medium house for $100000")
                assets.append("Medium house")
            elif balance < 100000:
                print("You don't have enough money to buy a medium house")
        elif choice == 3:
            if balance >= 500000:
                print("You bought a large house for $500000")
                assets.append("Large house")
            elif balance < 500000:
                print("You don't have enough money to buy a large house")
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)    
    elif choice == 2:
        print("1: Buy a a small car")
        print("2: Buy a medium car")
        print("3: Buy a large car")
        choice = int(input(""))
        if choice == 1:
            if balance >= 5000:
                print("You bought a small car for $5000")
                assets.append("Small car")
            elif balance < 5000:
                print("You don't have enough money to buy a small car")
        elif choice == 2:
            if balance >= 20000:
                print("You bought a medium car for $20000")
                assets.append("Medium car")
            elif balance < 20000:
                print("You don't have enough money to buy a medium car")
        elif choice == 3:
            if balance >= 100000:
                print("You bought a large car for $100000")
                assets.append("Large car")
            elif balance < 100000:
                print("You don't have enough money to buy a large car")
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    elif choice == 3:
        for i in range(len(assets)):
            print(assets[i])
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    elif choice == 4:
        menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
    else:
        print("Not a valid option")
        asset()
    return assets, balance
menu(meAge, occupation, smarts, happiness, jobAction, education, balance, yearsInCollege)
