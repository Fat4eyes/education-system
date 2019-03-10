import {TestTypes} from '../common/enums'

export default class Test {
  public Id?: number
  public Subject: string = ''
  public IsActive: boolean = false
  public TotalTime: number = 0
  public Attempts: number = 0
  public TestType: TestTypes = TestTypes.Control

//
// public bool IsActive { get; set; }
//
// public int DisciplineId { get; set; }
//
// public int? TotalTime { get; set; }
//
// public int? Attempts { get; set; }
//
// public TestType? Type { get; set; }
//
// public int? IsRandom { get; set; }
//
// public List<Theme> Themes { get; set; }
}