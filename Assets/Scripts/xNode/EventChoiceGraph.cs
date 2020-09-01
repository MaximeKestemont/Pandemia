using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class EventChoiceGraph : NodeGraph {
    public static int nextEventUid = 1;
    public static int nextDialogueUid = 1;

    public int CountNodes() {
        return nodes.Count;
    }

    public List<EventNode> GetEventNodes() {
      List<EventNode> result = new List<EventNode>();
      foreach (var node in nodes) {
        if (node is EventNode) {
          result.Add(node as EventNode);
        }
      }
      return result;
    }

    public List<ChoiceNode> GetChoiceNodes() {
      List<ChoiceNode> result = new List<ChoiceNode>();
      foreach (var node in nodes) {
        if (node is ChoiceNode) {
          result.Add(node as ChoiceNode);
        }
      }
      return result;
    }

    public List<NPCDialogueNode> GetDialogueNodes() {
      List<NPCDialogueNode> result = new List<NPCDialogueNode>();
      foreach (var node in nodes) {
        if (node is NPCDialogueNode) {
          result.Add(node as NPCDialogueNode);
        }
      }
      return result;      
    }
}