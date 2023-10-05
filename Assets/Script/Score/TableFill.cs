using UnityEngine;
using UnityEngine.UI;

public class TableFill : MonoBehaviour
{
    [SerializeField] GameObject Row;
    [SerializeField] GameObject Cell;

    [SerializeField] GameObject TableBody;
    [SerializeField] GameObject TableHeaderRow;
    [SerializeField] Text RecordsName;

    public void Start()
    {
        //�������� ������ ���� ��������
        SavedScore[] scores = ScoreController.getAllSavedScosres();
        if(scores.Length == 0)
        {
            gameObject.SetActive(false);
            RecordsName.text = "� ��� ���� ��� ��������";
        }
        else
        {
            gameObject.SetActive(true);
            RecordsName.text = "�������";
        }
        bool headerCreate = false;
        //�������� ������������ ����
        createHeaderCell("�");
        createHeaderCell("����:");
        //�������� ������� ��������
        int rowIndex = 1;
        foreach (SavedScore score in scores )
        {
            GameObject currentRow = Instantiate(Row, TableBody.transform);
            //�������� ������ � �������� � ����� � ������ ������
            createBodyCell(rowIndex.ToString(), currentRow.transform);
            createBodyCell(score.saveDate, currentRow.transform);
    
            foreach (Score oneScore in score.savedScore)
            {
                //�������� ������ �������
                if (!headerCreate)
                {
                    createHeaderCell(oneScore.scoreNameForMenu);
                }
                //�������� ���� �������
                createBodyCell(oneScore.scoreValue.ToString(), currentRow.transform);
            }
            if(!headerCreate) headerCreate = true;
            rowIndex++;
        }
    }

    private Text checkCell(GameObject cell)
    {
        Text cellText = cell.GetComponentInChildren<Text>();
        if (!cellText) throw new System.Exception("� ������ ������ ������� " + cell + "����������� ��������� Text!");
        return cellText;
    }

    private void createHeaderCell(string value)
    {
        GameObject headerCell = Instantiate(Cell, TableHeaderRow.transform);
        Text cellText = checkCell(headerCell);
        cellText.text = value;
    }

    private void createBodyCell(string value, Transform currentRow)
    {
        GameObject bodyCell = Instantiate (Cell, currentRow);
        Text bodyCellText = checkCell(bodyCell);
        bodyCellText.text = value;

    }

}
