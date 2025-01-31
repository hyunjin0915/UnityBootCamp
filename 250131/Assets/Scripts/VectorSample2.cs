using UnityEngine;

public class VectorSample2 : MonoBehaviour
{
    //VectorSample�� ���� �������ֽø� �����ϴ�. 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        1. Normalization(����ȭ)
         = ������ ũ�⸦ 1�� ����(���� ������ ������ ũ�⸸ 1�� ����)
        ũ�⸦ 1�� �����ϸ� ������ ���⸸ ����ϸ� �ż� ���� ���� ó���� ���� ���� 
        ex) �Է����� ���� ĳ������ 3D �̵��� �밢������ �̷���� ���
        �Ϲ����� ���� ���⺸�� �̵��ӵ��� �� ���� ��Ȳ�� �߻��� �� ���� 
        */
        Vector3 a = new Vector3(1, 2, 0);
        Vector3 normal_a = a.normalized;

        /*
         2. �� ���� ������ �Ÿ� ���
        �� ������ ������ ũ�Ⱑ ���� 
         */
        Vector3 positionA = new Vector3(1, 2, 3);
        Vector3 positionB = new Vector3(2, 3, 4);
        float distance = Vector3.Distance(positionA, positionB);

        /*
         3. ���� ����(Linear Interpolation -> Lerp)
        �� ���� ���� �����Ǿ��� �� �� ���̿� ��ġ�� ���� �����ϱ� ���� �����Ÿ��� ���� ���������� ����ϴ� ��� 
         ex) A������ (2,1) �̰� B������ (6,4)�� �� �� ������ ���� C�� x ��ǥ�� �־����ٸ� y ��ǥ���� �� �� ����

         */
        Vector3 Result = Vector3.Lerp(positionA, positionB, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
