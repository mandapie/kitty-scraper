# KittyScraper

I don't like the way PetFinder and AdoptAPet sites work. I only want to look for cats from adoption centers that I am familiar with. Hence, this project.
I set up a webhook to send a discord notification to a text channel when a cat I'm looking for is on a site.

To run the project, simply run the console app.\
Running with no arguments will scrape all cats.\
To search for cats, the argument is entered in the form of a JSON string.\
You can enter any criteria as long as it matches the `Cat` model from the project.\
Example:\
`"{'Breed':'siamese'}"`

Sample result:\
![image](https://github.com/user-attachments/assets/0dfb1089-192c-4ade-8786-acbb97472be7)

To automate scraping, I used Task Scheduler.

Only scrapes cats from the following adoption centers:
- SpcaLA
- Seal Beach Animal Care Center
- OC Animal Care

Let me know if there are other adoption centers around LA. I want to add them to my scrape collection.\
I wanted to add Long Beach Animal Care but can't scrape the breed from the listing page.
