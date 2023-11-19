using System.Collections.Generic;
/*
    대화 관련 CSV파일을 읽어와 딕셔너리로 변환합니다.
*/
public static class CSVConverter
{
    public static Dictionary<string, List<Dialogue>> GetDialogues(string CSVName)
    {
        return RefineData(ReadData(CSVName));
    }

    // CSV파일을 읽어 반환합니다.
    static List<Dictionary<string, object>> ReadData(string CSVName)
    {
        return CSVReader.Read("csv/" + CSVName);
    }

    // 대화 CSV파일을 정리하여 딕셔너리로 반환합니다.
    static Dictionary<string, List<Dialogue>> RefineData(List<Dictionary<string, object>> texts)
    {
        Dictionary<string, List<Dialogue>> temp = new Dictionary<string, List<Dialogue>>();
        for (int i = 0; i < texts.Count; i++)
        {
            string category = texts[i]["category"].ToString();

            Dialogue tempDialogue = new Dialogue();
            tempDialogue.type = texts[i]["type"].ToString();
            tempDialogue.name = RefineText(texts[i]["name"].ToString());
            tempDialogue.text = RefineText(texts[i]["text"].ToString());
            tempDialogue.action = texts[i]["action"].ToString();
            tempDialogue.nextCategory = texts[i]["nextCategory"].ToString();
            tempDialogue.mentalIndex = texts[i]["mental"].ToString() == "" ? 0 : int.Parse(texts[i]["mental"].ToString());

            if (!temp.ContainsKey(category))
            {
                List<Dialogue> tempDialogues = new List<Dialogue>();
                tempDialogues.Add(tempDialogue);
                temp.Add(category, tempDialogues);
            }
            else
            {
                temp[category].Add(tempDialogue);
            }
        }
        return temp;
    }

    // CSV파일에 사용된 특수문자들을 원래 의도했던 문자로 변환합니다. 예) @ -> ,
    static string RefineText(string tempString)
    {
        return tempString.Replace("@", ",").Replace("\\n", "\n").Replace("\"", "");
    }
}
